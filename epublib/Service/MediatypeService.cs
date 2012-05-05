using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nl.siegmann.epublib.domain;
using nl.siegmann.epublib.util;

namespace nl.siegmann.epublib.service
{
    public class MediatypeService
    {
        public static MediaType XHTML = new MediaType("application/xhtml+xml", ".xhtml", new String[] { ".htm", ".html", ".xhtml" });
        public static MediaType EPUB = new MediaType("application/epub+zip", ".epub");
        public static MediaType NCX = new MediaType("application/x-dtbncx+xml", ".ncx");

        public static MediaType JAVASCRIPT = new MediaType("text/javascript", ".js");
        public static MediaType CSS = new MediaType("text/css", ".css");

        // images
        public static MediaType JPG = new MediaType("image/jpeg", ".jpg", new String[] { ".jpg", ".jpeg" });
        public static MediaType PNG = new MediaType("image/png", ".png");
        public static MediaType GIF = new MediaType("image/gif", ".gif");

        public static MediaType SVG = new MediaType("image/svg+xml", ".svg");

        // fonts
        public static MediaType TTF = new MediaType("application/x-truetype-font", ".ttf");
        public static MediaType OPENTYPE = new MediaType("application/vnd.ms-opentype", ".otf");
        public static MediaType WOFF = new MediaType("application/font-woff", ".woff");

        // audio
        public static MediaType MP3 = new MediaType("audio/mpeg", ".mp3");
        public static MediaType MP4 = new MediaType("audio/mp4", ".mp4");

        public static MediaType SMIL = new MediaType("application/smil+xml", ".smil");
        public static MediaType XPGT = new MediaType("application/adobe-page-template+xml", ".xpgt");
        public static MediaType PLS = new MediaType("application/pls+xml", ".pls");

        public static MediaType[] mediatypes = new MediaType[] {
		XHTML, EPUB, JPG, PNG, GIF, CSS, SVG, TTF, NCX, XPGT, OPENTYPE, WOFF, SMIL, PLS, JAVASCRIPT, MP3, MP4
	    };

        public static Dictionary<String, MediaType> mediaTypesByName = new Dictionary<String, MediaType>();

        static MediatypeService()
        {
            for (int i = 0; i < mediatypes.Length; i++)
            {
                mediaTypesByName.Add(mediatypes[i].getName(), mediatypes[i]);
            }
        }

        public static bool isBitmapImage(MediaType mediaType)
        {
            return mediaType == JPG || mediaType == PNG || mediaType == GIF;
        }

        /**
         * Gets the MediaType based on the file extension.
         * Null of no matching extension found.
         * 
         * @param filename
         * @return
         */
        public static MediaType determineMediaType(String filename)
        {
            for (int i = 0; i < mediatypes.Length; i++)
            {
                MediaType mediatype = mediatypes[i];
                foreach (String extension in mediatype.getExtensions())
                {
                    if (StringUtil.endsWithIgnoreCase(filename, extension))
                    {
                        return mediatype;
                    }
                }
            }
            return null;
        }

        public static MediaType getMediaTypeByName(String mediaTypeName)
        {
            foreach (var item in mediaTypesByName)
            {
                if (mediaTypeName.Equals(item.Value))
                    return item.Value;
            }
            return null;
        }
    }
}
