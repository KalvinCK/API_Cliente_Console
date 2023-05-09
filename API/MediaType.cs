namespace ClientAPI.API
{
    public enum MediaType
    {
        TextPlain,
        TextHtml,
        ApplicationJson,
        ApplicationXML,
        ImagePng,
        ImageJpeg,
        AudioMpeg,
        VideoMp4,
    }
    public static class MediaTypeValue
    {
        public static string MediaToString(this MediaType value)
        {
            switch(value)
            {
                case MediaType.TextPlain:
                    return "text/plain";

                case MediaType.TextHtml:
                    return "text/html";

                case MediaType.ApplicationJson:
                    return "application/json";

                case MediaType.ApplicationXML:
                    return "application/xml";

                case MediaType.ImagePng:
                    return "image/png";

                case MediaType.ImageJpeg:
                    return "image/jpeg";

                case MediaType.AudioMpeg:
                    return "audio/mpeg";

                case MediaType.VideoMp4:
                    return "video/mp4";

                default:
                    return "";

            }
        }
    }
}

       
        