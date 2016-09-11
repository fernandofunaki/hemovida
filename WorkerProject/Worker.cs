using Domain.Core;
using Domain.HealthChecker;
using HeartBeat.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace WorkerProject
{
    public abstract class Worker
    {
        #region Events
        protected event OWorkerStatusChanged OnWorkerStatusChanged;
        #endregion

        #region Fields
        protected int WorkerId { get; set; }
        private bool KeepAlive { get; set; }
        private bool isRecoveryRequired { get; set; }
        private WorkerStatus _status;
        private readonly IHeartBeat _heartBeat;
        private readonly IHealthChecker _healthChecker;
        protected MailBox _mailBox;
        #endregion

        #region Properties
        public WorkerStatus Status
        {
            get { return _status; }
            set
            {
                _status = value;
                WorkerStatusChanged(value);
            }
        }
        #endregion

        #region Constructures
        public Worker(IHeartBeat heartBeat, IHealthChecker healthChecker)
        {
            KeepAlive = true;
            this._heartBeat = heartBeat;
            this._healthChecker = healthChecker;
        }
        #endregion

        #region Protected Methods

        protected virtual void BuildUp() { }

        protected abstract MessageRequest ReadMessage();

        protected abstract void Recover();

        protected abstract void SendMessage(MessageRequest message);

        #endregion

        #region Public Methods
        public void Start()
        {
            try
            {
                this.BuildUp();

                this.Initialise();

                while (this.KeepAlive)
                {
                    this.SleepWorker();

                    try
                    {
                        if (this.KeepAlive)
                        {
                            this.Status = WorkerStatus.Processing;
                            this._healthChecker.StartCapture(HealthCheckerType.Working);
                            this.Process();
                            this._healthChecker.StopCapture(HealthCheckerType.Stopped);
                            this.Status = WorkerStatus.Processed;
                        }
                    }
                    catch (Exception ex)
                    {
                        this._healthChecker.StartCapture(HealthCheckerType.Failed);
                        this.Status = WorkerStatus.Failed;
                        this.Log(ex);
                        this.Status = WorkerStatus.Recovering;
                        this.Recover();
                        this.Status = WorkerStatus.Recovered;
                        this._healthChecker.StopCapture(HealthCheckerType.Failed);
                    }
                    finally
                    {
                        this.Status = WorkerStatus.AwaitingJob;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Log(ex);
            }
        }

        public void Stop()
        {
            _heartBeat.Stop();
            _healthChecker.Finalizer();
        }


        #endregion

        #region Private Methods
        private void SleepWorker()
        {
            System.Threading.Thread.Sleep(2000);
        }

        private void WorkerStatusChanged(WorkerStatus message)
        {
            if (OnWorkerStatusChanged != null)
            {
                OnWorkerStatusChanged(message);
            }
        }

        protected abstract void Process();

        private void Initialise()
        {
            this.Status = WorkerStatus.Initialized;
            _heartBeat.Start(this.WorkerId);
            _healthChecker.Initialize(this.WorkerId);
        }

        private void Log(Exception ex)
        {

        }

        #endregion
    }
}

