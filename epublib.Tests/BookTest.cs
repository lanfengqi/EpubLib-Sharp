using nl.siegmann.epublib.domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using nl.siegmann.epublib.service;

namespace epublib.Tests
{


    /// <summary>
    ///This is a test class for BookTest and is intended
    ///to contain all BookTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BookTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod]
        public void testGetContents1()
        {
            Book book = new Book();
            Resource resource1 = new Resource("id1", System.Text.Encoding.UTF8.GetBytes("Hello, world !"), "chapter1.html", MediatypeService.XHTML);
            book.getSpine().addResource(resource1);
            book.getTableOfContents().addSection(resource1, "My first chapter");
            Assert.AreEqual(1, book.getContents().Count);
        }
         [TestMethod]
        public void testGetContents2()
        {
            Book book = new Book();
            Resource resource1 = new Resource("id1", System.Text.Encoding.UTF8.GetBytes("Hello, world !"), "chapter1.html", MediatypeService.XHTML);
            book.getSpine().addResource(resource1);
            Resource resource2 = new Resource("id1",System.Text.Encoding.UTF8.GetBytes("Hello, world !"), "chapter2.html", MediatypeService.XHTML);
            book.getTableOfContents().addSection(resource2, "My first chapter");
            Assert.AreEqual(2, book.getContents().Count);
        }
         [TestMethod]
        public void testGetContents3()
        {
            Book book = new Book();
            Resource resource1 = new Resource("id1", System.Text.Encoding.UTF8.GetBytes("Hello, world !"), "chapter1.html", MediatypeService.XHTML);
            book.getSpine().addResource(resource1);
            Resource resource2 = new Resource("id1", System.Text.Encoding.UTF8.GetBytes("Hello, world !"), "chapter2.html", MediatypeService.XHTML);
            book.getTableOfContents().addSection(resource2, "My first chapter");
            book.getGuide().addReference(new GuideReference(resource2, GuideReference.FOREWORD, "The Foreword"));
            Assert.AreEqual(2, book.getContents().Count);
        }
         [TestMethod]
        public void testGetContents4()
        {
            Book book = new Book();

            Resource resource1 = new Resource("id1", System.Text.Encoding.UTF8.GetBytes("Hello, world !"), "chapter1.html", MediatypeService.XHTML);
            book.getSpine().addResource(resource1);

            Resource resource2 = new Resource("id1", System.Text.Encoding.UTF8.GetBytes("Hello, world !"), "chapter2.html", MediatypeService.XHTML);
            book.getTableOfContents().addSection(resource2, "My first chapter");

            Resource resource3 = new Resource("id1", System.Text.Encoding.UTF8.GetBytes("Hello, world !"), "foreword.html", MediatypeService.XHTML);
            book.getGuide().addReference(new GuideReference(resource3, GuideReference.FOREWORD, "The Foreword"));

            Assert.AreEqual(3, book.getContents().Count);
        }

        /// <summary>
        ///A test for Book Constructor
        ///</summary>
        [TestMethod()]
        public void BookConstructorTest()
        {
            Book target = new Book();
            //Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
