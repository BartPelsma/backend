﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Models
{
    /// <summary>
    /// <b>User</b> class used for EF Core to map its landscape for the database.
    /// </summary>
    public class User
    {
        /// <summary>
        /// [Key]: Identification key for a User entry in the User table
        /// [Required]: cannot be null
        /// Identification key
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Info on the user, this is extern data
        /// Currently it includes Name, Studentnumber and a Password
        /// </summary>
        public UserInfo UserInfo { get; set; }

        /// <summary>
        /// Token used to refresh the login session of the user.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// A date will be set when the user is banned
        /// Will be null when the user is not banned
        /// </summary>
        public DateTime? BannedUntil { get; set; }

        /// <summary>
        /// [Required]: cannot be null
        /// Used to know what role the users has
        /// Refrences to a Role class
        /// </summary>
        [Required]
        public Role Role { get; set; }
    }
}
