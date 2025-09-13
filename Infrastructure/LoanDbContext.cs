using Microsoft.EntityFrameworkCore;
using Mini_Loan_App.Domain;

namespace Mini_Loan_App.Infrastructure;

public sealed class LoanDbContext: DbContext
{
    public LoanDbContext(DbContextOptions<LoanDbContext> options): base(options) {}
    
    public DbSet<Loan> Loans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Loan>(e =>
        {
            e.ToTable("loans");
            e.HasKey(x => x.Id);
            e.Property(x => x.ApplicantName).HasMaxLength(200).IsRequired();
            e.Property(x => x.Principal).HasPrecision(18, 2);
            e.Property(x => x.AnnualRate).HasPrecision(9, 6);
            e.Property(x => x.TermMonths).IsRequired();
            e.Property(x => x.CreatedAt).HasColumnType("timestamp with time zone");
            e.Property(x => x.Status).HasConversion<string>().HasMaxLength(20).IsRequired();
        });
    }

}