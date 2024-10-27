using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace plan02.Models
{
    public partial class PlannedStaffManagementContext : DbContext
    {
        public PlannedStaffManagementContext()
        {
        }

        public PlannedStaffManagementContext(DbContextOptions<PlannedStaffManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AEmployee> AEmployees { get; set; }
        public virtual DbSet<AUser> AUsers { get; set; }
        public virtual DbSet<Assessment> Assessments { get; set; }
        public virtual DbSet<BEmployee> BEmployees { get; set; }
        public virtual DbSet<BUser> BUsers { get; set; }
        public virtual DbSet<CaptchaResult> CaptchaResults { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<EfmigrationsHistory> EfmigrationsHistories { get; set; }
        public virtual DbSet<LoginViewModel> LoginViewModels { get; set; }
        public virtual DbSet<Persalaryview> Persalaryviews { get; set; }
        public virtual DbSet<Persassessview> Persassessviews { get; set; }
        public virtual DbSet<Perseduiview> Perseduiviews { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Pertransview> Pertransviews { get; set; }
        public virtual DbSet<PlanTable> PlanTables { get; set; }
        public virtual DbSet<PunchIn> PunchIns { get; set; }
        public virtual DbSet<Salary> Salaries { get; set; }
        public virtual DbSet<SalaryAssessment> SalaryAssessments { get; set; }
        public virtual DbSet<SignOn> SignOns { get; set; }
        public virtual DbSet<SingleSignOn> SingleSignOns { get; set; }
        public virtual DbSet<TEmployee> TEmployees { get; set; }
        public virtual DbSet<Transfer> Transfers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserDatum> UserData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            modelBuilder.Entity<AEmployee>(entity =>
            {
                entity.HasKey(e => e.AId)
                    .HasName("PRIMARY");

                entity.ToTable("aEmployee");

                entity.Property(e => e.AId)
                    .HasColumnType("int(50)")
                    .ValueGeneratedNever()
                    .HasColumnName("aId")
                    .HasComment("員工編號");

                entity.Property(e => e.AEmploymentDate)
                    .HasColumnType("date")
                    .HasColumnName("aEmploymentDate")
                    .HasComment("雇用日期");

                entity.Property(e => e.AGender)
                    .HasMaxLength(50)
                    .HasColumnName("aGender")
                    .HasComment("性別");

                entity.Property(e => e.AMail)
                    .HasMaxLength(50)
                    .HasColumnName("aMail")
                    .HasComment("電子信箱");

                entity.Property(e => e.AName)
                    .HasMaxLength(50)
                    .HasColumnName("aName")
                    .HasComment("姓名");

                entity.Property(e => e.ASalary)
                    .HasColumnType("int(11)")
                    .HasColumnName("aSalary")
                    .HasComment("薪資");

                entity.Property(e => e.UserName)
                    .HasMaxLength(255)
                    .HasComment("帳號");
            });

            modelBuilder.Entity<AUser>(entity =>
            {
                entity.HasKey(e => e.Aid)
                    .HasName("PRIMARY");

                entity.ToTable("aUser");

                entity.Property(e => e.Aid).HasColumnType("int(100) unsigned");

                entity.Property(e => e.Aaccount)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("帳號");

                entity.Property(e => e.Aidentity)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("身分證字號");

                entity.Property(e => e.Apassword)
                    .HasMaxLength(255)
                    .HasComment("密碼");

                entity.Property(e => e.StoredSalt).HasMaxLength(255);
            });

            modelBuilder.Entity<Assessment>(entity =>
            {
                entity.HasKey(e => e.Aid)
                    .HasName("PRIMARY");

                entity.ToTable("Assessment");

                entity.Property(e => e.Aid)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.AssessmentIndex).HasMaxLength(255);

                entity.Property(e => e.AssessmentYear).HasMaxLength(255);

                entity.Property(e => e.CreateTime).HasMaxLength(255);

                entity.Property(e => e.Identity).HasMaxLength(255);

                entity.Property(e => e.LastChangeName).HasMaxLength(255);

                entity.Property(e => e.LastChangeTime).HasMaxLength(255);
            });

            modelBuilder.Entity<BEmployee>(entity =>
            {
                entity.HasKey(e => e.BId)
                    .HasName("PRIMARY");

                entity.ToTable("bEmployee");

                entity.HasIndex(e => e.UserName, "Username1")
                    .IsUnique();

                entity.Property(e => e.BId)
                    .HasColumnType("int(100) unsigned")
                    .HasColumnName("bId")
                    .HasComment("員工編號");

                entity.Property(e => e.BCreatetime)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("bCreatetime")
                    .HasComment("創建時間");

                entity.Property(e => e.BEmploymentDate)
                    .HasColumnType("date")
                    .HasColumnName("bEmploymentDate")
                    .HasComment("雇用日期");

                entity.Property(e => e.BEmploymentDate2)
                    .HasColumnType("date")
                    .HasColumnName("bEmploymentDate2")
                    .HasComment("起聘結束日期");

                entity.Property(e => e.BGender)
                    .HasMaxLength(50)
                    .HasColumnName("bGender")
                    .HasComment("性別");

                entity.Property(e => e.BMail)
                    .HasMaxLength(50)
                    .HasColumnName("bMail")
                    .HasComment("電子信箱");

                entity.Property(e => e.BName)
                    .HasMaxLength(50)
                    .HasColumnName("bName")
                    .HasComment("姓名");

                entity.Property(e => e.BSalary)
                    .HasColumnType("int(100)")
                    .HasColumnName("bSalary")
                    .HasComment("薪資");

                entity.Property(e => e.BType)
                    .HasMaxLength(150)
                    .HasColumnName("bType")
                    .HasComment("助理人員類別");

                entity.Property(e => e.UserName).HasComment("帳號");
            });

            modelBuilder.Entity<BUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("bUser");

                entity.HasIndex(e => e.UserName, "Username")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "bUser_UserId_IDX")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnType("int(46) unsigned");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(46)
                    .HasComment("密碼");

                entity.Property(e => e.StoredSalt).HasMaxLength(256);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(46)
                    .HasComment("帳號");
            });

            modelBuilder.Entity<CaptchaResult>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CaptchaResult");

                entity.Property(e => e.CaptchBase64Data).HasMaxLength(100);

                entity.Property(e => e.CaptchaByteData).HasMaxLength(100);

                entity.Property(e => e.CaptchaCode)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Timestamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("current_timestamp()");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Education>(entity =>
            {
                entity.HasKey(e => e.Eid)
                    .HasName("PRIMARY");

                entity.ToTable("Education");

                entity.Property(e => e.Eid)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateTime).HasMaxLength(255);

                entity.Property(e => e.EducationDepartment).HasMaxLength(255);

                entity.Property(e => e.EducationEnd).HasMaxLength(255);

                entity.Property(e => e.EducationLevel).HasMaxLength(255);

                entity.Property(e => e.EducationName).HasMaxLength(255);

                entity.Property(e => e.EducationStart).HasMaxLength(255);

                entity.Property(e => e.EducationYear).HasMaxLength(255);

                entity.Property(e => e.Identity).HasMaxLength(255);

                entity.Property(e => e.LastChangeName).HasMaxLength(255);

                entity.Property(e => e.LastChangeTime).HasMaxLength(255);
            });

            modelBuilder.Entity<EfmigrationsHistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__EFMigrationsHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<LoginViewModel>(entity =>
            {
                entity.HasKey(e => e.Lid)
                    .HasName("PRIMARY");

                entity.ToTable("LoginViewModel");

                entity.HasIndex(e => e.Lid, "Lid")
                    .IsUnique();

                entity.Property(e => e.Lid).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(48);
            });

            modelBuilder.Entity<Persalaryview>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("PRIMARY");

                entity.ToTable("Persalaryview");

                entity.Property(e => e.Sid)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateTime).HasMaxLength(255);

                entity.Property(e => e.Identity).HasMaxLength(255);

                entity.Property(e => e.LastChangeName).HasMaxLength(255);

                entity.Property(e => e.LastChangeTime).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.SalarYear).HasMaxLength(255);

                entity.Property(e => e.SalaryIndex).HasMaxLength(255);

                entity.Property(e => e.Salaryscale).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.Unit).HasMaxLength(255);
            });

            modelBuilder.Entity<Persassessview>(entity =>
            {
                entity.HasKey(e => e.Aid)
                    .HasName("PRIMARY");

                entity.ToTable("Persassessview");

                entity.Property(e => e.Aid)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.AssessmentIndex).HasMaxLength(255);

                entity.Property(e => e.AssessmentYear).HasMaxLength(255);

                entity.Property(e => e.CreateTime).HasMaxLength(255);

                entity.Property(e => e.Identity).HasMaxLength(255);

                entity.Property(e => e.LastChangeName).HasMaxLength(255);

                entity.Property(e => e.LastChangeTime).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.Unit).HasMaxLength(255);
            });

            modelBuilder.Entity<Perseduiview>(entity =>
            {
                entity.HasKey(e => e.Eid)
                    .HasName("PRIMARY");

                entity.ToTable("Perseduiview");

                entity.Property(e => e.Eid)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateTime).HasMaxLength(255);

                entity.Property(e => e.EducationDepartment).HasMaxLength(255);

                entity.Property(e => e.EducationEnd).HasMaxLength(255);

                entity.Property(e => e.EducationLevel).HasMaxLength(255);

                entity.Property(e => e.EducationName).HasMaxLength(255);

                entity.Property(e => e.EducationStart).HasMaxLength(255);

                entity.Property(e => e.EducationYear).HasMaxLength(255);

                entity.Property(e => e.Identity).HasMaxLength(255);

                entity.Property(e => e.LastChangeName).HasMaxLength(255);

                entity.Property(e => e.LastChangeTime).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.Unit).HasMaxLength(255);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Pid)
                    .HasName("PRIMARY");

                entity.ToTable("Person");

                entity.Property(e => e.Pid)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Aborignal).HasMaxLength(255);

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.AssessmentIndex).HasMaxLength(255);

                entity.Property(e => e.AssessmentYear).HasMaxLength(255);

                entity.Property(e => e.Birthdate).HasMaxLength(255);

                entity.Property(e => e.CreateTime).HasMaxLength(255);

                entity.Property(e => e.Disability).HasMaxLength(255);

                entity.Property(e => e.EducationDepartment).HasMaxLength(255);

                entity.Property(e => e.EducationEnd).HasMaxLength(255);

                entity.Property(e => e.EducationLevel).HasMaxLength(255);

                entity.Property(e => e.EducationName).HasMaxLength(255);

                entity.Property(e => e.EducationStart).HasMaxLength(255);

                entity.Property(e => e.EducationYear).HasMaxLength(255);

                entity.Property(e => e.EmergencyPerson).HasMaxLength(255);

                entity.Property(e => e.EmergencyTel).HasMaxLength(255);

                entity.Property(e => e.Identity).HasMaxLength(255);

                entity.Property(e => e.LastChangeName).HasMaxLength(255);

                entity.Property(e => e.LastChangeTime).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.OndutyDate).HasMaxLength(255);

                entity.Property(e => e.OntheJob).HasMaxLength(255);

                entity.Property(e => e.Other).HasMaxLength(255);

                entity.Property(e => e.ResignDate).HasMaxLength(255);

                entity.Property(e => e.SalarYear).HasMaxLength(255);

                entity.Property(e => e.SalaryIndex).HasMaxLength(255);

                entity.Property(e => e.Salaryscale).HasMaxLength(255);

                entity.Property(e => e.Sex).HasMaxLength(255);

                entity.Property(e => e.Tel).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.TransferIndex).HasMaxLength(255);

                entity.Property(e => e.TransferYear).HasMaxLength(255);

                entity.Property(e => e.Unit).HasMaxLength(255);
            });

            modelBuilder.Entity<Pertransview>(entity =>
            {
                entity.HasKey(e => e.Tid)
                    .HasName("PRIMARY");

                entity.ToTable("pertransview");

                entity.Property(e => e.Tid)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateTime).HasMaxLength(255);

                entity.Property(e => e.Identity).HasMaxLength(255);

                entity.Property(e => e.LastChangeName).HasMaxLength(255);

                entity.Property(e => e.LastChangeTime).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.TransferIndex).HasMaxLength(255);

                entity.Property(e => e.TransferYear).HasMaxLength(255);

                entity.Property(e => e.Unit).HasMaxLength(255);
            });

            modelBuilder.Entity<PlanTable>(entity =>
            {
                entity.HasKey(e => e.Aid)
                    .HasName("PRIMARY");

                entity.ToTable("planTable");

                entity.HasIndex(e => e.Bid, "BId");

                entity.Property(e => e.Aid)
                    .HasColumnType("int(11) unsigned")
                    .HasColumnName("AId")
                    .HasComment("key值");

                entity.Property(e => e.Assistant)
                    .IsRequired()
                    .HasMaxLength(24)
                    .HasColumnName("assistant")
                    .IsFixedLength(true)
                    .HasComment("助理人員類別");

                entity.Property(e => e.Bid)
                    .IsRequired()
                    .HasColumnName("BId")
                    .HasComment("校內計畫編號");

                entity.Property(e => e.Cid)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("科技部計畫編號");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("createTime")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'")
                    .HasComment("創建時間");

                entity.Property(e => e.Degree)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("degree")
                    .IsFixedLength(true)
                    .HasComment("學歷");

                entity.Property(e => e.PlanName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("planName")
                    .HasComment("計畫名稱");

                entity.Property(e => e.Salary)
                    .HasColumnType("int(11)")
                    .HasColumnName("salary")
                    .HasComment("薪資");

                entity.Property(e => e.Teacher)
                    .IsRequired()
                    .HasMaxLength(18)
                    .HasColumnName("teacher")
                    .IsFixedLength(true)
                    .HasComment("計畫主持人");

                entity.Property(e => e.Work)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("work")
                    .HasComment("工作內容");

                entity.Property(e => e.Workend)
                    .HasColumnType("date")
                    .HasColumnName("workend")
                    .HasComment("聘期結束");

                entity.Property(e => e.Workstart)
                    .HasColumnType("date")
                    .HasColumnName("workstart")
                    .HasComment("聘期開始");
            });

            modelBuilder.Entity<PunchIn>(entity =>
            {
                entity.HasKey(e => e.Pid)
                    .HasName("PRIMARY");

                entity.ToTable("PunchIn");

                entity.Property(e => e.Pid).HasColumnType("int(100) unsigned");

                entity.Property(e => e.Day)
                    .HasColumnType("int(100)")
                    .HasColumnName("day");

                entity.Property(e => e.Month)
                    .HasColumnType("int(100)")
                    .HasColumnName("month");

                entity.Property(e => e.PunchIn1)
                    .HasColumnType("timestamp")
                    .HasColumnName("PunchIn")
                    .HasComment("簽到");

                entity.Property(e => e.PunchOut)
                    .HasColumnType("timestamp")
                    .HasComment("簽退");

                entity.Property(e => e.Remark2)
                    .HasMaxLength(150)
                    .HasColumnName("remark2")
                    .HasComment("備註假別");

                entity.Property(e => e.Year)
                    .HasColumnType("int(100)")
                    .HasColumnName("year");
                entity.Property(e => e.Weekday)
                    .HasMaxLength(120)
                    .HasColumnName("weekday");
            });
            
            modelBuilder.Entity<Salary>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("PRIMARY");

                entity.ToTable("Salary");

                entity.Property(e => e.Sid)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateTime).HasMaxLength(255);

                entity.Property(e => e.Identity).HasMaxLength(255);

                entity.Property(e => e.LastChangeName).HasMaxLength(255);

                entity.Property(e => e.LastChangeTime).HasMaxLength(255);

                entity.Property(e => e.SalarYear).HasMaxLength(255);

                entity.Property(e => e.SalaryIndex).HasMaxLength(255);

                entity.Property(e => e.Salaryscale).HasMaxLength(255);
            });

            modelBuilder.Entity<SalaryAssessment>(entity =>
            {
                entity.HasKey(e => e.Said)
                    .HasName("PRIMARY");

                entity.ToTable("SalaryAssessment");

                entity.Property(e => e.Said)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.AssessmentIndex).HasMaxLength(255);

                entity.Property(e => e.AssessmentYear).HasMaxLength(255);

                entity.Property(e => e.CreateTime).HasMaxLength(255);

                entity.Property(e => e.Identity).HasMaxLength(255);

                entity.Property(e => e.LastChangeName).HasMaxLength(255);

                entity.Property(e => e.LastChangeTime).HasMaxLength(255);

                entity.Property(e => e.SalarYear).HasMaxLength(255);

                entity.Property(e => e.SalaryIndex).HasMaxLength(255);

                entity.Property(e => e.Salaryscale).HasMaxLength(255);
            });

            modelBuilder.Entity<SignOn>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("PRIMARY");

                entity.ToTable("SignOn");

                entity.Property(e => e.Sid).HasColumnType("int(100) unsigned");

                entity.Property(e => e.Remark)
                    .HasMaxLength(150)
                    .HasColumnName("remark")
                    .HasComment("備註假別");

                entity.Property(e => e.SignIn)
                    .HasColumnType("timestamp")
                    .HasComment("簽到");

                entity.Property(e => e.SignOut)
                    .HasColumnType("timestamp")
                    .HasComment("簽退");
            });

            modelBuilder.Entity<SingleSignOn>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SingleSignOn");

                entity.Property(e => e.Cn)
                    .HasMaxLength(255)
                    .HasColumnName("cn");

                entity.Property(e => e.LastChangeTime).HasMaxLength(255);

                entity.Property(e => e.LoginDisable).HasMaxLength(255);

                entity.Property(e => e.sAMAccountName)
                    .HasMaxLength(255)
                    .HasColumnName("sAMAccountName");
            });

            modelBuilder.Entity<TEmployee>(entity =>
            {
                entity.HasKey(e => e.FEmpId)
                    .HasName("PRIMARY");

                entity.ToTable("tEmployee");

                entity.Property(e => e.FEmpId)
                    .HasMaxLength(50)
                    .HasColumnName("fEmpId")
                    .HasComment("員工編號/主索引鍵");

                entity.Property(e => e.FEmploymentDate)
                    .HasMaxLength(50)
                    .HasColumnName("fEmploymentDate");

                entity.Property(e => e.FGender)
                    .HasMaxLength(50)
                    .HasColumnName("fGender")
                    .HasComment("性別");

                entity.Property(e => e.FName)
                    .HasMaxLength(50)
                    .HasColumnName("fName")
                    .HasComment("姓名");

                entity.Property(e => e.FSalary)
                    .HasColumnType("int(11)")
                    .HasColumnName("fSalary")
                    .HasComment("薪資");
            });

            modelBuilder.Entity<Transfer>(entity =>
            {
                entity.HasKey(e => e.Tid)
                    .HasName("PRIMARY");

                entity.ToTable("Transfer");

                entity.Property(e => e.Tid)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateTime).HasMaxLength(255);

                entity.Property(e => e.Identity).HasMaxLength(255);

                entity.Property(e => e.LastChangeName).HasMaxLength(255);

                entity.Property(e => e.LastChangeTime).HasMaxLength(255);

                entity.Property(e => e.TransferIndex).HasMaxLength(255);

                entity.Property(e => e.TransferYear).HasMaxLength(255);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PRIMARY");

                entity.ToTable("User");

                entity.Property(e => e.Uid).HasColumnType("int(11) unsigned");

                entity.Property(e => e.Account).HasMaxLength(255);

                entity.Property(e => e.CaptchaByteData).HasColumnType("blob");

                entity.Property(e => e.CaptchaCode).HasMaxLength(255);

                entity.Property(e => e.ChangeName).HasMaxLength(255);

                entity.Property(e => e.ChangeTime).HasMaxLength(255);

                entity.Property(e => e.CreateTime).HasMaxLength(255);

                entity.Property(e => e.Identity).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.Unit).HasMaxLength(255);
            });

            modelBuilder.Entity<UserDatum>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("userData");

                entity.HasIndex(e => e.IdNumber, "userData_UN")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11) unsigned")
                    .HasColumnName("userId");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("createTime")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'")
                    .HasComment("創建時間");

                entity.Property(e => e.IdNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("idNumber")
                    .HasComment("身分證字號");

                entity.Property(e => e.Tel)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasComment("手機號碼");

                entity.Property(e => e.UEmployeeTime)
                    .HasColumnType("datetime")
                    .HasColumnName("uEmployeeTime")
                    .HasComment("聘用日期");

                entity.Property(e => e.UGender)
                    .IsRequired()
                    .HasMaxLength(46)
                    .HasColumnName("uGender")
                    .HasComment("性別");

                entity.Property(e => e.UName)
                    .IsRequired()
                    .HasMaxLength(46)
                    .HasColumnName("uName")
                    .HasComment("姓名");

                entity.Property(e => e.USalary)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("uSalary")
                    .HasComment("薪資");

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasComment("單位");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("帳號");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
