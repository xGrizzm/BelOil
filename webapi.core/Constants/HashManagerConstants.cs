namespace webapi.core.Constants
{
    public static class HashManagerConstants
    {
        public const int SaltByteSize = 21;

        public const int SaltBase64Size = SaltByteSize / 3 + SaltByteSize;
    }
}
