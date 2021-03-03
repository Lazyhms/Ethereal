﻿using Ethereal.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ethereal.App.Models
{
    public class Tests
    {
        //[Requisite]
        public DateTime? Create { get; set; }

        [ModelBinder()]
        public DateTimeOffset? Timestamp { get; set; }

        public Guid Id { get; set; }

        [IDCard]
        public string IDCard { get; set; }

        [SocialCreditCode]
        public string SocialCreditCode { get; set; }

        [FileExtensions]
        public string FileExtensions { get; set; }

        [Required(ErrorMessage = "字段是必须的")]
        public IEnumerable<int> IEnumerableLength { get; set; }

        [FileLength(1)]
        public IFormFile File { get; set; }
    }
}
