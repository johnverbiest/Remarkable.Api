using System;

namespace JohnVerbiest.Remarkable.Api.ServiceClient.Dto
{
    internal class ListingItem
    {
        /// <summary>
        /// The Unique Id of this object. Always required
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Item's version, count start at 1
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// API Replies store error messages here
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// If the api call was an success
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Download URL for this object
        /// </summary>
        public string BlobURLGet { get; set; }

        /// <summary>
        /// Expiretime for the blob URL
        /// </summary>
        public DateTime BlobUrlGetExpires { get; set; }

        /// <summary>
        /// The last modified date time as set on the client that created the item
        /// </summary>
        public DateTime ModifiedClient { get; set; }

        /// <summary>
        /// The type of the object
        /// </summary>
        public ObjectType Type { get; set; }

        /// <summary>
        /// The currently open page, starting at 0
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Is the object bookmarked
        /// </summary>
        public bool Bookmarked { get; set; }

        /// <summary>
        /// The Id of the parent object for this object. Empty for a root level object.
        /// </summary>
        public Guid? Parent { get; set; }
    }
}
 