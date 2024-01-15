using Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapping
{
    public class ImageMap : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasData(new Image
            {
                Id = Guid.Parse("3BD57681-D153-4934-83F7-3458A9716648"),
                FileName = "images/TestImage",
                FileType = "jpg",
                CreatedBy = "admin",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            },
            new Image
            {
                Id = Guid.Parse("F144F00B-5A1E-4624-A3CC-DD26B2E0CEE9"),
                FileName = "images/TestImage2",
                FileType = "jpg",
                CreatedBy = "admin",
                CreatedDate = DateTime.Now,
                IsDeleted = false,

            });
        }
    }
}
