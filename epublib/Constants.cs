using System;

namespace nl.siegmann.epublib
{
    public static class Constants
    {
        /// <summary>
        /// 编码格式
        /// </summary>
        public static string ENCODING ="uft-8";
        /// <summary>
        /// xhtml 类型
        /// </summary>
        public static string DOCTEYE_XHTML = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD XHTML 1.1//EN\" \"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd\">";
        /// <summary>
        /// xhtml命名空间
        /// </summary>
        public static string NAMESPACE_XHTML = "http://www.w3.org/1999/xhtml";
        /// <summary>
        /// EPUBLIB 版本
        /// </summary>
        public static String EPUBLIB_GENERATOR_NAME = "EPUBLib version 3.0";
        /// <summary>
        /// 片段分离字符
        /// </summary>
        public static char FRAGMENT_SEPARATOR_CHAR = '#';
        /// <summary>
        /// 默认TocID
        /// </summary>
        public static String DEFAULT_TOC_ID = "toc";
    }
}
