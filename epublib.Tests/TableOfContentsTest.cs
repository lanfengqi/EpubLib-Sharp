using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using nl.siegmann.epublib.domain;

namespace epublib.Tests
{
    [TestClass]
    public class TableOfContentsTest
    {
        [TestMethod]
        public void testCalculateDepth_simple1()
        {
            TableOfContents tableOfContents = new TableOfContents();
            Assert.AreEqual(0, tableOfContents.calculateDepth());
        }
        [TestMethod]
        public void testCalculateDepth_simple2()
        {
            TableOfContents tableOfContents = new TableOfContents();
            tableOfContents.addTOCReference(new TOCReference());
            Assert.AreEqual(1, tableOfContents.calculateDepth());
        }
        [TestMethod]
        public void testCalculateDepth_simple3()
        {
            TableOfContents tableOfContents = new TableOfContents();
            tableOfContents.addTOCReference(new TOCReference());
            TOCReference childTOCReference = tableOfContents.addTOCReference(new TOCReference());
            childTOCReference.addChildSection(new TOCReference());
            tableOfContents.addTOCReference(new TOCReference());

            Assert.AreEqual(2, tableOfContents.calculateDepth());
        }
        [TestMethod]
        public void testAddResource1()
        {
            Resource resource = new Resource("foo");
            TableOfContents toc = new TableOfContents();
            TOCReference tocReference = toc.addSection(resource, "apple/pear", "/");
            Assert.IsNotNull(tocReference);
            Assert.IsNotNull(tocReference.getResource());
            Assert.AreEqual(2, toc.size());
            Assert.AreEqual("pear", tocReference.getTitle());
        }
        [TestMethod]
        public void testAddResource2()
        {
            Resource resource = new Resource("foo");
            TableOfContents toc = new TableOfContents();
            TOCReference tocReference = toc.addSection(resource, "apple/pear", "/");
            Assert.IsNotNull(tocReference);
            Assert.IsNotNull(tocReference.getResource());
            Assert.AreEqual(2, toc.size());
            Assert.AreEqual("pear", tocReference.getTitle());

            TOCReference tocReference2 = toc.addSection(resource, "apple/banana", "/");
            Assert.IsNotNull(tocReference2);
            Assert.IsNotNull(tocReference2.getResource());
            Assert.AreEqual(3, toc.size());
            Assert.AreEqual("banana", tocReference2.getTitle());

            TOCReference tocReference3 = toc.addSection(resource, "apple", "/");
            Assert.IsNotNull(tocReference3);
            Assert.IsNotNull(tocReference.getResource());
            Assert.AreEqual(3, toc.size());
            Assert.AreEqual("apple", tocReference3.getTitle());
        }
        [TestMethod]
        public void testAddResource3()
        {
            Resource resource = new Resource("foo");
            TableOfContents toc = new TableOfContents();
            TOCReference tocReference = toc.addSection(resource, "apple/pear");
            Assert.IsNotNull(tocReference);
            Assert.IsNotNull(tocReference.getResource());
            Assert.AreEqual(1, toc.getTocReferences().Count);
            Assert.AreEqual(1, toc.getTocReferences()[0].getChildren().Count);
            Assert.AreEqual(2, toc.size());
            Assert.AreEqual("pear", tocReference.getTitle());
        }
        [TestMethod]
        public void testAddResourceWithIndexes()
        {
            Resource resource = new Resource("foo");
            TableOfContents toc = new TableOfContents();
            TOCReference tocReference = toc.addSection(resource, new int[] { 0, 0 }, "Section ", ".");

            // check newly created TOCReference
            Assert.IsNotNull(tocReference);
            Assert.IsNotNull(tocReference.getResource());
            Assert.AreEqual("Section 1.1", tocReference.getTitle());

            // check table of contents
            Assert.AreEqual(1, toc.getTocReferences().Count);
            Assert.AreEqual(1, toc.getTocReferences()[0].getChildren().Count);
            Assert.AreEqual(2, toc.size());
            Assert.AreEqual("Section 1", toc.getTocReferences()[0].getTitle());
            Assert.AreEqual("Section 1.1", toc.getTocReferences()[0].getChildren()[0].getTitle());
            Assert.AreEqual(1, toc.getTocReferences()[0].getChildren().Count);
        }
    }
}
