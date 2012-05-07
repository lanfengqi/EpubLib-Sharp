using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using nl.siegmann.epublib.domain;
using nl.siegmann.epublib.service;

namespace epublib.Tests
{
    [TestClass]
    public class ResourcesTest
    {
        [TestMethod]
        public void testGetResourcesByMediaType1()
        {
            Resources resources = new Resources();
            resources.add(new Resource( System.Text.Encoding.UTF8.GetBytes("foo"), MediatypeService.XHTML));
            resources.add(new Resource(System.Text.Encoding.UTF8.GetBytes("bar"), MediatypeService.XHTML));
            Assert.AreEqual(0, resources.getResourcesByMediaType(MediatypeService.PNG).Count);
            Assert.AreEqual(2, resources.getResourcesByMediaType(MediatypeService.XHTML).Count);
            Assert.AreEqual(2, resources.getResourcesByMediaTypes(new MediaType[] { MediatypeService.XHTML }).Count);
        }

        [TestMethod]
        public void testGetResourcesByMediaType2()
        {
            Resources resources = new Resources();
            resources.add(new Resource(System.Text.Encoding.UTF8.GetBytes("foo"), MediatypeService.XHTML));
            resources.add(new Resource(System.Text.Encoding.UTF8.GetBytes("bar"), MediatypeService.PNG));
            resources.add(new Resource(System.Text.Encoding.UTF8.GetBytes("baz"), MediatypeService.PNG));
            Assert.AreEqual(2, resources.getResourcesByMediaType(MediatypeService.PNG).Count);
            Assert.AreEqual(1, resources.getResourcesByMediaType(MediatypeService.XHTML).Count);
            Assert.AreEqual(1, resources.getResourcesByMediaTypes(new MediaType[] { MediatypeService.XHTML }).Count);
            Assert.AreEqual(3, resources.getResourcesByMediaTypes(new MediaType[] { MediatypeService.XHTML, MediatypeService.PNG }).Count);
            Assert.AreEqual(3, resources.getResourcesByMediaTypes(new MediaType[] { MediatypeService.CSS, MediatypeService.XHTML, MediatypeService.PNG }).Count);
        }
    }
}
