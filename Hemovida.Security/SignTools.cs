using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Hemovida.Security
{
    public class SignTools
    {
        public static bool VerifySignatureXML(X509Certificate2 x509Cert, XmlDocument doc)
        {

            RSACryptoServiceProvider Key = (RSACryptoServiceProvider)x509Cert.PrivateKey;

            Reference reference = new Reference();
            reference.Uri = "";

            // Cria o elemento XML para assinatura
            SignedXml signedXml = new SignedXml(doc);

            //Adiciona a chave privada para assinar o documento
            signedXml.SigningKey = x509Cert.PrivateKey;


            // Find the "Signature" node and create a new
            // XmlNodeList object.
            XmlNodeList nodeList = doc.GetElementsByTagName("Signature");

            // Load the signature node.
            signedXml.LoadXml((XmlElement)nodeList[0]);

            // Check the signature and return the result.
            return signedXml.CheckSignature(Key);
        }

        public static string CreateSignatureXML(X509Certificate2 x509Cert, XmlDocument doc) {

            Reference reference = new Reference();
            reference.Uri = "";

            // Cria o elemento XML para assinatura
            SignedXml signedXml = new SignedXml(doc);

            //Adiciona a chave privada para assinar o documento
            signedXml.SigningKey = x509Cert.PrivateKey;

            // Adicionar tags da assinatura dentro do elemento referencia, padrão W3C
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            //Representa C14N XML - Padrao W3C
            XmlDsigC14NTransform c14 = new XmlDsigC14NTransform();
            reference.AddTransform(c14);

            // adiciona o elemento referencia na assinatura
            signedXml.AddReference(reference);

            // Cria uma KeyInfo 
            KeyInfo keyInfo = new KeyInfo();

            // Carrega o certificado dentro de um KeyInfoX509Data
            // e adiciona na KeyInfo
            keyInfo.AddClause(new KeyInfoX509Data(x509Cert));

            // Adiciona a KeyInfo na assinatura
            signedXml.KeyInfo = keyInfo;

            //Cria assinatura
            signedXml.ComputeSignature();

            // Obter o XMl com a assinatura
            // it to an XmlElement object.
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            doc.GetElementsByTagName("Assinatura")[0].AppendChild(xmlDigitalSignature);

            return doc.OuterXml;
        }

        public static string CreateHash(X509Certificate2 cert, string texto)
        {
            string result = string.Empty;
            try
            {
                //Converte o xml em bytes
                byte[] sAssinaturaByte = Encoding.ASCII.GetBytes(texto);

                //Cria instancia do SHA1
                var sha1 = System.Security.Cryptography.SHA1.Create();

                //Cria o Hash da mensagem
                byte[] hashBytes = sha1.ComputeHash(sAssinaturaByte);

                //Convert Hash gerado em string64
                result = Convert.ToBase64String(hashBytes);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public static string SignHash(X509Certificate2 cert, string texto)
        {
            string result = string.Empty;

            byte[] sAssinaturaByte = Encoding.ASCII.GetBytes(texto);

            //Cria instancia do SHA1
            var sha1 = System.Security.Cryptography.SHA1.Create();

            //Cria o Hash da mensagem
            byte[] hashBytes = sha1.ComputeHash(sAssinaturaByte);

            //Provider para realizar  criptografia assimetrica
            RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)cert.PrivateKey;

            //Cria a formatação da assiantura de hash
            RSAPKCS1SignatureFormatter rsaF = new RSAPKCS1SignatureFormatter(rsa);

            //Define o tipo de algoritimo para criar a assinatura
            rsaF.SetHashAlgorithm("SHA1");

            //Criptografa o hash 
            byte[] assinado = rsaF.CreateSignature(hashBytes);

            //converte para string 64
            return Convert.ToBase64String(assinado);
        }

        public static string Encrypt(X509Certificate2 cert, string dataToDycript)
        {
            RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)cert.PublicKey.Key;
            return Convert.ToBase64String(rsa.Encrypt(ASCIIEncoding.ASCII.GetBytes(dataToDycript), true));
        }

        public static string Decrypt(X509Certificate2 cert, string encryptedData)
        {
            RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)cert.PrivateKey;
            return ASCIIEncoding.ASCII.GetString(rsa.Decrypt(Convert.FromBase64String(encryptedData), true));
        }
    }
}
