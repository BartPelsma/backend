﻿namespace ProductService.Models
{
    /// <summary>
    /// Data model for the AddImage calls
    /// </summary>
    public class AddImageModel
    {
        /// <summary>
        /// Contains primarykey of product or note
        /// </summary>
        public int LinkedPrimaryKey { get; set; }
        /// <summary>
        /// Contains info if LinkedPrimaryKey is linked to a product or note
        /// </summary>
        public LinkedTableType LinkedTableType { get; set; }
        /// <summary>
        /// Contains all images that need to be saved
        /// </summary>
        public string[] Base64Images { get; set; }

        public AddImageModel(int linkedPrimaryKey, LinkedTableType linkedTableType, string[] base64Images)
        {
            LinkedPrimaryKey = linkedPrimaryKey;
            LinkedTableType = linkedTableType;
            Base64Images = base64Images;
        }
    }
}
