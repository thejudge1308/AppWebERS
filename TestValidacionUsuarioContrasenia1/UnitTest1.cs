using System;
using AppWebERS.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestValidacionUsuarioContrasenia1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            String usuario = "";
            String contrasenia = "23131";

            Assert.AreEqual(false,new LoginController().permitirAccesoUsuario(usuario,contrasenia));
        }
        [TestMethod]
        public void TestMethod2()
        {
            String usuario = "432423";
            String contrasenia = "";

            Assert.AreEqual(false, new LoginController().permitirAccesoUsuario(usuario, contrasenia));
        }
        [TestMethod]
        public void TestMethod3()
        {
            String usuario = " ";
            String contrasenia = "23131";

            Assert.AreEqual(false, new LoginController().permitirAccesoUsuario(usuario, contrasenia));
        }
        [TestMethod]
        public void TestMethod4()
        {
            String usuario = "123123";
            String contrasenia = "2 3131 ";

            Assert.AreEqual(false, new LoginController().permitirAccesoUsuario(usuario, contrasenia));
        }
        [TestMethod]
        public void TestMethod5()
        {
            String usuario = "63464664564564564565456";
            String contrasenia = "23131";

            Assert.AreEqual(false, new LoginController().permitirAccesoUsuario(usuario, contrasenia));
        }
        [TestMethod]
        public void TestMethod6()
        {
            String usuario = "23";
            String contrasenia = "2313lfhakdladhasjkdhaskdhasfbbf1";

            Assert.AreEqual(false, new LoginController().permitirAccesoUsuario(usuario, contrasenia));
        }
        [TestMethod]
        public void TestMethod7()
        {
            String usuario = "18884898";
            String contrasenia = "NFS0128";

            Assert.AreEqual(true, new LoginController().permitirAccesoUsuario(usuario, contrasenia));
        }
    }
}
