using FullTextSearchApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullTextSearchApi.Data.EntityConfigs;

public class  bInvertedIndexConfig : IEntityTypeConfiguration<InvertedIndex>
{
    public void Configure(EntityTypeBuilder<InvertedIndex> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();
        builder.HasIndex(c => c.Word);
    }
}