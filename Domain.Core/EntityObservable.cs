using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PropertyChangedEventArgs : EventArgs
    {
        private string _propertyName;


        // Summary:
        //     Initializes a new instance of the System.ComponentModel.PropertyChangedEventArgs
        //     class.
        //
        // Parameters:
        //   propertyName:
        //     The name of the property that changed.
        public PropertyChangedEventArgs(string propertyName)
        {
            _propertyName = propertyName;
        }

        // Summary:
        //     Gets the name of the property that changed.
        //
        // Returns:
        //     The name of the property that changed.
        public virtual string PropertyName
        {
            get
            {
                return _propertyName;
            }
        }
    }

    public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);

    public interface INotifyPropertyChanged
    {
        // Summary:
        //     Occurs when a property value changes.
        event PropertyChangedEventHandler PropertyChanged;
    }

    [Serializable]
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// this dictionary holds all changed files.
        /// </summary>
        private Dictionary<string, object> originalValues;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableObject"/> class.
        /// </summary>
        protected ObservableObject()
        {
        }

        #endregion

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this instance has changed.
        /// The system are using this instance to hold changes in forms that
        /// cannot be close before save. Call ClearHasChanged when the
        /// form be saved.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance has changed; otherwise, <c>false</c>.
        /// </value>
        public virtual bool HasChanged
        {
            get
            {
                return this.originalValues != null && this.originalValues.Count > 0;
            }
        }

        /// <summary>
        /// Gets a value indicating whether an exception is thrown, or if a Debug.Fail() is used
        /// when an invalid property name is passed to the VerifyPropertyName method.
        /// The default value is false, but subclasses used by unit tests might
        /// override this property's getter to return true.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This is an API so this is used by classes outside of this dll.")]
        protected virtual bool ThrowOnInvalidPropertyName
        {
            get;
            private set;
        }

        #endregion Properties

        #region Members

        /// <summary>
        /// Clears the has changed.
        /// </summary>
        public void ClearHasChanged()
        {
            if (this.originalValues != null)
            {
                this.originalValues.Clear();
            }
        }

        public void RemoveOriginalValue(string propertyName)
        {
            if (this.originalValues != null)
            {
                this.originalValues.Remove(propertyName);
            }
        }

        /// <summary>
        /// Raises the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "It's not an event")]
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Sets the supplied target to the value passed in. Then if the value is different raises
        /// the property changed event.
        /// </summary>
        /// <remarks>
        /// This is used to implement logic as a property is changed. The main use is to track 
        /// changes to the object.
        /// </remarks>
        /// <typeparam name="T">The type of the property</typeparam>
        /// <param name="target">The target passed in as a reference.</param>
        /// <param name="value">The new value.</param>
        /// <param name="propertyName">Name of the property.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#",
            Justification = "Acceptable pattern within MVVM.")]
        protected void SetValue<T>(ref T target, T value, string propertyName)
        {
            if ((target == null ^ value == null) || (target != null && !target.Equals(value)))
            {
                if (this.originalValues == null)
                {
                    this.originalValues = new Dictionary<string, object>();
                }

                if (!this.originalValues.ContainsKey(propertyName))
                {
                    this.originalValues.Add(propertyName, target);
                }
                else if ((this.originalValues[propertyName] == null && (value == null || value.ToString().Length == 0)) || (this.originalValues[propertyName] != null && this.originalValues[propertyName].Equals(value)))
                {
                    this.originalValues.Remove(propertyName);
                }

                target = value;

                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }

                this.RaisePropertyChanged("HasChanged");
            }
        }

        /// <summary>
        /// Warns the developer if this object does not have
        /// a public property with the specified name. This
        /// method does not exist in a Release build.
        /// </summary>
        /// <remarks>
        /// This method is only included in DEBUG builds.
        /// </remarks>
        /// <param name="propertyName">Name of the property.</param>
        /// <exception cref="ArgumentException">Thrown if propertyName is not a
        /// valid property and ThrowOnInvalidPropertyName is true.</exception>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        protected void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = string.Format(CultureInfo.InvariantCulture, "Invalid {0} {1}.", "Property Name", propertyName);

                if (this.ThrowOnInvalidPropertyName)
                {
                    throw new ArgumentException(msg);
                }
                else
                {
                    Debug.Fail(msg);
                }
            }
        }

        #endregion
    }


    [Serializable]
    public abstract class ValidatingObservableObject : ObservableObject, IDataErrorInfo
    {
        #region Constructor


        protected ValidatingObservableObject()
        {
        }

        #endregion


        public virtual string Error
        {
            get
            {
                StringBuilder errorsStringBuilder = new StringBuilder();

                this.GetType()
                    .GetProperties()
                    .ToList()
                    .ForEach(p => errorsStringBuilder.Append(Validate(this, p)));

                return errorsStringBuilder.ToString();
            }
        }


        public virtual string this[string columnName]
        {
            get
            {
                return Validate(this, this.GetType().GetProperty(columnName));
            }
        }


        public virtual IEnumerable<ValidationAttribute> GetValidationAttributes(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttributes(typeof(ValidationAttribute), true)
                             .Cast<ValidationAttribute>()
                             .ToList<ValidationAttribute>();
        }


        public virtual string Validate(object instance, System.Reflection.PropertyInfo propertyInfo)
        {
            var error = string.Empty;

            foreach (ValidationAttribute va in GetValidationAttributes(propertyInfo))
            {
                if (va.IsValid(propertyInfo.GetValue(instance, null)) == false)
                {
                    error += va.FormatErrorMessage(propertyInfo.GetDisplayName());
                    error += Environment.NewLine;
                }
            }

            return error.Trim();
        }
    }

    public static class PropertyInfoExtensions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "We only want this to work on Properties.")]
        public static string GetDisplayName(this PropertyInfo value)
        {
            var displayNameAttribute =
                value.GetCustomAttributes(true).OfType<DisplayNameAttribute>().SingleOrDefault();

            if (displayNameAttribute == null)
            {
                return value.Name;
            }
            else
            {
                return displayNameAttribute.DisplayName;
            }
        }
    }


    [Serializable]
    public abstract class Entity : ValidatingObservableObject
    {

        private byte[] timestamp;


        [DataFieldMapping("Timestamp")]
        public virtual byte[] Timestamp
        {
            get
            {
                return (byte[])this.timestamp.Clone();
            }

            set
            {
                byte[] valueAux = (byte[])value.Clone();
                if (this.timestamp != valueAux)
                {
                    this.timestamp = valueAux;
                    this.RaisePropertyChanged("Timestamp");
                }
            }
        }

        [DataFieldMapping("Country_Code")]
        public virtual string CountryCode { get; set; }
   
    }


    [Serializable]
    public class EntityExample : Entity
    {

        private string localSfkAccountNumber;



        [DisplayName("Sfk Account Number")]
        [DataFieldMapping("Sfk_AC_Nbr")]
        [Required]
        [StringLength(34)]
        public string SfkAccountNumber
        {
            get
            {
                return this.localSfkAccountNumber;
            }
            set { base.SetValue<string>(ref this.localSfkAccountNumber, value, "SfkAccountNumber"); }
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class FilterOnlyAttribute : Attribute
    {
    }

    //Entender como funciona na GTS Common.
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DataFieldMappingAttribute : Attribute
    {
        public DataFieldMappingAttribute(string dataField) { }
        public DataFieldMappingAttribute(string dataField, Type dataType, object defaultValue) { }

        public string DataField { get { return ""; } }
        public Type DataType { get { return Type.GetType(""); } }
        public object DefaultValue { get { return new Object(); } }
    }
}
