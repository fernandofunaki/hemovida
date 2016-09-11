using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Core;
using System.IO;
using System.Xml.Serialization;
using Hemovida.Security;
using System.Security.Cryptography.X509Certificates;
using Hemovida.XMLTools;
using System.Xml;
using System.Diagnostics;

namespace SignXMLTest
{
    [TestClass]
    public class HashTests
    {
        private Message mensagemOriginal;
        private X509Certificate2 certificate;
        private XmlDocument xmlDoc;

        [TestInitialize]
        public void Iniciar()
        {
            xmlDoc = new XmlDocument();
            mensagemOriginal = new Message();
            mensagemOriginal.ContentMessage.CNPJ = "12121123000100";
            mensagemOriginal.ContentMessage.EmitenteId = "A13G0-12313-00212-12317";
            mensagemOriginal.ContentMessage.EmitenteNome = "Laboratório Emilio Ribas";
            mensagemOriginal.ContentMessage.EventoId = "UPDAGENDA";

            certificate = new X509Certificate2("c://temp/certificate/cert.pfx", "xxxx",
                X509KeyStorageFlags.PersistKeySet |
                X509KeyStorageFlags.MachineKeySet |
                X509KeyStorageFlags.Exportable);
        }

        [TestMethod]
        public void AssinarMensagemXmlTest()
        {
            //Será utilizada para assinatura
            XmlDocument doc = new XmlDocument();

            //Obtem mensagem xml
            string xml = XmlTools.ToXmlString<Message>(mensagemOriginal);

            //Carrega o xml
            doc.LoadXml(xml);

            //Assina o xml
            string xmlAssinado = SignTools.CreateSignatureXML(certificate, doc);

            //Carrega Xml para checar o conteudo do elemento Assinatura
            doc.LoadXml(xmlAssinado);

            //Verifica se foi gerada a assinatura
            Assert.IsTrue(doc.GetElementsByTagName("SignatureValue").Count.Equals(1));
        }

        [TestMethod]
        public void VerificarAssinaturaDigitalValidaTest()
        {
            //Será utilizada para assinatura
            XmlDocument doc = new XmlDocument();

            //Obtem mensagem xml
            string xml = XmlTools.ToXmlString<Message>(mensagemOriginal);

            //Carrega o xml
            doc.LoadXml(xml);

            //Assina o xml
            string xmlAssinado = SignTools.CreateSignatureXML(certificate, doc);

            //Carrega Xml para checar o conteudo do elemento Assinatura
            doc.LoadXml(xmlAssinado);

            var result = SignTools.VerifySignatureXML(certificate, doc);

            //Verifica se foi gerada a assinatura
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void VerificarAssinaturaDigitalInvalidaTest()
        {
            //Será utilizada para assinatura
            XmlDocument doc = new XmlDocument();

            //Obtem mensagem xml
            string xml = XmlTools.ToXmlString<Message>(mensagemOriginal);

            //Carrega o xml
            doc.LoadXml(xml);

            //Assina o xml
            string xmlAssinado = SignTools.CreateSignatureXML(certificate, doc);

            //Carrega Xml para checar o conteudo do elemento Assinatura
            doc.LoadXml(xmlAssinado);

            //Alterando o valor da assinatura para verificar se ainda permanece válido
            doc.GetElementsByTagName("SignatureValue").Item(0).InnerText = doc.GetElementsByTagName("SignatureValue").Item(0).InnerText.Replace('0', '1');

            //Verifica a assinatura
            var result = SignTools.VerifySignatureXML(certificate, doc);

            //Verifica se foi gerada a assinatura
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GerarHashTest()
        {
            //obtem xml da mensagem original
            string mensagemXml = XmlTools.ToXmlString<Message>(mensagemOriginal);

            //Carrega em um XmlDocument para obter o conteudo da mensagem
            xmlDoc.LoadXml(mensagemXml);

            //Obtem o conteudo da mensagem para gerar o hash
            string messageContentElement = xmlDoc.GetElementsByTagName("ContentMessage").Item(0).OuterXml;

            //gera o hash do conteudo da mensagem
            string hashGerado = SignTools.CreateHash(certificate, messageContentElement);

            //Inclui o Hash Gerado na tabela Hash
            xmlDoc.GetElementsByTagName("Hash").Item(0).InnerText = hashGerado;

            //verifica se o hash foi gerado
            Assert.IsFalse(string.IsNullOrEmpty(hashGerado));
        }

        [TestMethod]
        public void GerarHashEAssinarTest()
        {
            //obtem xml da mensagem original
            string mensagemXml = XmlTools.ToXmlString<Message>(mensagemOriginal);

            //Carrega em um XmlDocument para obter o conteudo da mensagem
            xmlDoc.LoadXml(mensagemXml);

            //Obtem o conteudo da mensagem para gerar o hash
            string messageContentElement = xmlDoc.GetElementsByTagName("ContentMessage").Item(0).OuterXml;

            //gera o hash do conteudo da mensagem
            string hashGerado = SignTools.CreateHash(certificate, messageContentElement);

            //Inclui o Hash Gerado na tabela Hash
            xmlDoc.GetElementsByTagName("Hash").Item(0).InnerText = hashGerado;

            //Assina o xml
            string xmlAssinado = SignTools.CreateSignatureXML(certificate, xmlDoc);

            //Carrega Xml para checar o conteudo do elemento Assinatura
            xmlDoc.LoadXml(xmlAssinado);

            //Verifica se foi gerada a assinatura
            Assert.IsTrue(xmlDoc.GetElementsByTagName("SignatureValue").Count.Equals(1));
        }


        [TestMethod]
        public void ValidarMensagemAdulteradaTest()
        {
            Stopwatch timer = new Stopwatch();
           

            //Obtem XML da mensagem A (Original)
            string mensagemOriginalXML = XmlTools.ToXmlString<Message>(mensagemOriginal);
            xmlDoc.LoadXml(mensagemOriginalXML);

            //Obtem o conteudo da mensagem original
            mensagemOriginalXML = xmlDoc.GetElementsByTagName("ContentMessage").Item(0).OuterXml;

            //Gera o HASH da mensagem original
            string hashMensagemOriginal = SignTools.CreateHash(certificate, mensagemOriginalXML);

            //Encripta Hash da mensagem original
            hashMensagemOriginal = SignTools.Encrypt(certificate, hashMensagemOriginal);

            //Desencripta Hash da mensagem original
            hashMensagemOriginal = SignTools.Decrypt(certificate, hashMensagemOriginal);

            //Altera CNPJ que será utilizado na mensagem B
            mensagemOriginal.ContentMessage.CNPJ = "111111111111";

            //Obtem XML da mensagem B (Adulterada)
            string mensagemAdulteradaXML = XmlTools.ToXmlString<Message>(mensagemOriginal);
            xmlDoc.LoadXml(mensagemAdulteradaXML);

            timer.Start();

            //Obtem o conteudo da mensagem adulterada
            mensagemAdulteradaXML = xmlDoc.GetElementsByTagName("ContentMessage").Item(0).OuterXml;

            //Gera o HASH da mensagem adulterada
            string hashMensagemAdulterada = SignTools.CreateHash(certificate, mensagemAdulteradaXML);

            //Encripta Hash da mensagem adulterada
            hashMensagemAdulterada = SignTools.Encrypt(certificate, hashMensagemAdulterada);

            //Desencripta Hash da mensagem original
            hashMensagemAdulterada = SignTools.Decrypt(certificate, hashMensagemAdulterada);

            timer.Stop();
            long time = timer.ElapsedMilliseconds;

            //Checa se os hashs são iguais
            Assert.AreNotEqual(hashMensagemOriginal, hashMensagemAdulterada);
        }

        [TestMethod]
        public void ValidarMensagemNaoAdulteradaTest()
        {
            //Obtem XML da mensagem A (Original)
            string mensagemOriginalXML = XmlTools.ToXmlString<Message>(mensagemOriginal);

            //Gera o HASH da mensagem original
            string hashMensagemOriginal = SignTools.CreateHash(certificate, mensagemOriginalXML);

            //Encripta Hash da mensagem original
            hashMensagemOriginal = SignTools.Encrypt(certificate, hashMensagemOriginal);

            //Desencripta Hash da mensagem original
            hashMensagemOriginal = SignTools.Decrypt(certificate, hashMensagemOriginal);

            //Obtem XML da mensagem B (Adulterada)
            string mensagemAdulteradaXML = XmlTools.ToXmlString<Message>(mensagemOriginal);

            //Gera o HASH da mensagem adulterada
            string hashMensagemAdulterada = SignTools.CreateHash(certificate, mensagemAdulteradaXML);

            //Encripta Hash da mensagem adulterada
            hashMensagemAdulterada = SignTools.Encrypt(certificate, hashMensagemAdulterada);

            //Desencripta Hash da mensagem original
            hashMensagemAdulterada = SignTools.Decrypt(certificate, hashMensagemAdulterada);

            //Checa se os hashs são iguais
            Assert.AreEqual(hashMensagemOriginal, hashMensagemAdulterada);
        }

        [TestMethod]
        public void ValidarCriptografiaDoHashTest()
        {
            //Texto para ser criptografado
            string textoOriginal = "ABC123";

            //Criptografa texto
            string textoCriptografado = SignTools.Encrypt(certificate, textoOriginal);

            //Decriptografa texto
            string textoDecriptografado = SignTools.Decrypt(certificate, textoCriptografado);

            //Verifica se o texto é valido
            Assert.AreNotEqual(textoOriginal, textoCriptografado);
        }
    }
}
