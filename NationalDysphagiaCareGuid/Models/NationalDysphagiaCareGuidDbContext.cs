using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NationalDysphagiaCareGuid.Models;

public partial class NationalDysphagiaCareGuidDbContext : DbContext
{
    public NationalDysphagiaCareGuidDbContext()
    {
    }

    public NationalDysphagiaCareGuidDbContext(DbContextOptions<NationalDysphagiaCareGuidDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientCurrentState> PatientCurrentStates { get; set; }

    public virtual DbSet<PatientHistory> PatientHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=.\\Database\\NationalDysphagiaCareGuid_DB.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasIndex(e => e.PatientId, "IX_Patients_patientId").IsUnique();

            entity.Property(e => e.PatientId).HasColumnName("patientId");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.FirstName).HasColumnName("firstName");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.LastName).HasColumnName("lastName");
            entity.Property(e => e.PhoneNumber).HasColumnName("phoneNumber");
        });

        modelBuilder.Entity<PatientCurrentState>(entity =>
        {
            entity.HasKey(e => e.CurrentStateId);

            entity.ToTable("PatientCurrentState");

            entity.HasIndex(e => e.CurrentStateId, "IX_PatientCurrentState_currentStateId").IsUnique();

            entity.Property(e => e.CurrentStateId).HasColumnName("currentStateId");
            entity.Property(e => e.CoughingChokingWhileEating).HasColumnName("coughingChokingWhileEating");
            entity.Property(e => e.DroolingWhileEating).HasColumnName("droolingWhileEating");
            entity.Property(e => e.FeelingChestBurningAfterEating).HasColumnName("feelingChestBurningAfterEating");
            entity.Property(e => e.FeelingFoodStuckingInChest).HasColumnName("feelingFoodStuckingInChest");
            entity.Property(e => e.FeelingLikeFoodStuckedInThroat).HasColumnName("feelingLikeFoodStuckedInThroat");
            entity.Property(e => e.FeelingVomitingAfterEating).HasColumnName("feelingVomitingAfterEating");
            entity.Property(e => e.HoldingFoodInMouthForLongTime).HasColumnName("holdingFoodInMouthForLongTime");
            entity.Property(e => e.PainWhileSwallowing).HasColumnName("painWhileSwallowing");
            entity.Property(e => e.Patient).HasColumnName("patient");
            entity.Property(e => e.RespiratoryDistressWhileEating).HasColumnName("respiratoryDistressWhileEating");
            entity.Property(e => e.UnableToSwallowTheMedication).HasColumnName("unableToSwallowTheMedication");

            entity.HasOne(d => d.PatientNavigation).WithMany(p => p.PatientCurrentStates).HasForeignKey(d => d.Patient);
        });

        modelBuilder.Entity<PatientHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId);

            entity.ToTable("PatientHistory");

            entity.HasIndex(e => e.HistoryId, "IX_PatientHistory_historyId").IsUnique();

            entity.Property(e => e.HistoryId).HasColumnName("historyId");
            entity.Property(e => e.Cancer).HasColumnName("cancer");
            entity.Property(e => e.Dysphagia).HasColumnName("dysphagia");
            entity.Property(e => e.FeedingMode).HasColumnName("feedingMode");
            entity.Property(e => e.GastrologicalIssue).HasColumnName("gastrologicalIssue");
            entity.Property(e => e.PastSurgeriesDescription).HasColumnName("pastSurgeriesDescription");
            entity.Property(e => e.Patient).HasColumnName("patient");
            entity.Property(e => e.RespiratoryIssue).HasColumnName("respiratoryIssue");
            entity.Property(e => e.Stroke).HasColumnName("stroke");
            entity.Property(e => e.SurgeriesInPast).HasColumnName("surgeriesInPast");

            entity.HasOne(d => d.PatientNavigation).WithMany(p => p.PatientHistories).HasForeignKey(d => d.Patient);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
