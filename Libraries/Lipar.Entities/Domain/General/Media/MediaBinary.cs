namespace Lipar.Entities.Domain.General
{
    public class MediaBinary : BaseEntity
    {
        public int MediaId { get; set; }
        public byte[] BinaryData { get; set; }

        #region Navigation
        public Media Media { get; set; }
        #endregion
    }
}
