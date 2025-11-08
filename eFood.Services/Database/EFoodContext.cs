using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eFood.Services.Database;

public partial class EFoodContext : DbContext
{
    public EFoodContext()
    {
    }

    public EFoodContext(DbContextOptions<EFoodContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Dojmovi> Dojmovis { get; set; }

    public virtual DbSet<Drzava> Drzavas { get; set; }

    public virtual DbSet<Grad> Grads { get; set; }

    public virtual DbSet<Jelo> Jelos { get; set; }

    public virtual DbSet<Kategorija> Kategorijas { get; set; }

    public virtual DbSet<Korisnici> Korisnicis { get; set; }

    public virtual DbSet<KorisniciUloge> KorisniciUloges { get; set; }

    public virtual DbSet<Narudzba> Narudzbas { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<StavkeNarudzbe> StavkeNarudzbes { get; set; }

    public virtual DbSet<Uloge> Uloges { get; set; }

    public virtual DbSet<Uplata> Uplata { get; set; }
    public virtual DbSet<Restoran> Restorans { get; set; }
    public virtual DbSet<Korpa> Korpas { get; set; }
    public virtual DbSet<Prilozi> Prilozis { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-HOR3B84;Initial Catalog=eFood;User ID=sa;Password=Lozinka123;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dojmovi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Dojmovi__3214EC078DD4709C");

            entity.ToTable("Dojmovi");

            entity.Property(e => e.Opis).HasMaxLength(100);
            // entity.Property(e => e.DatumRecenzije).HasColumnType("datetime");

            entity.HasOne(d => d.Jelo).WithMany(p => p.Dojmovis)
                .HasForeignKey(d => d.JeloId)
                .HasConstraintName("FK__Dojmovi__JeloId__36B12243")
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.Korisnik).WithMany(p => p.Dojmovis)
                .HasForeignKey(d => d.KorisnikId)
                .HasConstraintName("FK__Dojmovi__Korisni__37A5467C").OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Drzava>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Drzava__3214EC076CFB6820");

            entity.ToTable("Drzava");

            entity.Property(e => e.Naziv).HasMaxLength(20);
        });

        modelBuilder.Entity<Grad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Grad__3214EC07EA4B5287");

            entity.ToTable("Grad");

            entity.Property(e => e.Naziv).HasMaxLength(50);
            entity.Property(e => e.DrzavaId).HasMaxLength(50);

        });
        modelBuilder.Entity<Prilozi>(entity =>
        {
            entity.HasKey(e => e.PrilogId).HasName("PK__Prilozi__3214EC07EA4B5287");

            entity.ToTable("Prilozi");

            entity.Property(e => e.NazivPriloga).HasMaxLength(50);

        });

        modelBuilder.Entity<Jelo>(entity =>
        {
            entity.HasKey(e => e.JeloId).HasName("PK__Jelo__3214EC0766C6F668");

            entity.ToTable("Jelo");

            entity.Property(e => e.Cijena).HasColumnType("money");
            entity.Property(e => e.Naziv).HasMaxLength(100);
            entity.Property(e => e.Opis).HasMaxLength(100);

            entity.HasOne(d => d.Kategorija).WithMany(p => p.Jelos)
                .HasForeignKey(d => d.KategorijaId)
                .HasConstraintName("FK__Jelo__Kategorija__2E1BDC42");
        });

        modelBuilder.Entity<Kategorija>(entity =>
        {
            entity.HasKey(e => e.KategorijaId).HasName("PK__Kategori__3214EC070A4F4900");

            entity.ToTable("Kategorija");

            entity.Property(e => e.Naziv).HasMaxLength(50);
            entity.Property(e => e.Opis).HasMaxLength(100);
        });

        modelBuilder.Entity<Korisnici>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Korisnic__3214EC07033A56BF");

            entity.ToTable("Korisnici");

            entity.Property(e => e.Ime).HasMaxLength(100);
            entity.Property(e => e.KorisnickoIme).HasMaxLength(100);
            entity.Property(e => e.LozinkaHash).HasMaxLength(256);
            entity.Property(e => e.LozinkaSalt).HasMaxLength(256);
            entity.Property(e => e.Prezime).HasMaxLength(100);

        });

        modelBuilder.Entity<KorisniciUloge>(entity =>
        {
            entity.HasKey(e => e.KorisnikUlogaId).HasName("PK__KorisniciUloge__1608726E");

            entity.ToTable("KorisniciUloge");

            entity.Property(e => e.DatumIzmjene).HasColumnType("datetime");

            entity.HasOne(d => d.Korisnik)
                .WithMany(p => p.KorisniciUloges)
                .HasForeignKey(d => d.KorisnikId)
                .HasConstraintName("FK__Korisnici__Koris__4316F928");

            entity.HasOne(d => d.Uloga)
                .WithMany(p => p.KorisniciUloges)
                .HasForeignKey(d => d.UlogaId)
                .HasConstraintName("FK__Korisnici__Uloga__440B1D61");
        });


        modelBuilder.Entity<Narudzba>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Narudzba__3214EC07423966FD");

            entity.ToTable("Narudzba");

            entity.Property(e => e.DatumNarudzbe).HasColumnType("datetime");

            entity.HasOne(d => d.Korisnik).WithMany(p => p.Narudzbas)
                .HasForeignKey(d => d.KorisnikId)
                .HasConstraintName("FK__Narudzba__Korisn__32E0915F");

            entity.HasOne(d => d.StatusNarudzbe).WithMany(p => p.Narudzbas)
                .HasForeignKey(d => d.StatusNarudzbeId)
                .HasConstraintName("FK__Narudzba__Status__33D4B598");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Status__3214EC071A49A4A8");

            entity.ToTable("Status");

            entity.Property(e => e.Naziv).HasMaxLength(50);
        });

        modelBuilder.Entity<StavkeNarudzbe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StavkeNa__3214EC0781E952BB");

            entity.ToTable("StavkeNarudzbe");

            entity.HasOne(d => d.Jelo).WithMany(p => p.StavkeNarudzbes)
                .HasForeignKey(d => d.JeloId)
                .HasConstraintName("FK__StavkeNar__JeloI__3A81B327");

            entity.HasOne(d => d.Narudzba).WithMany(p => p.StavkeNarudzbes)
                .HasForeignKey(d => d.NarudzbaId)
                .HasConstraintName("FK__StavkeNar__Narud__3B75D760");
        });
        modelBuilder.Entity<Korpa>().HasKey(z => z.KorpaId);
        modelBuilder.Entity<Korpa>(entity =>
        {
            entity.ToTable("Korpa");


            entity.Property(e => e.Kolicina).HasMaxLength(20);
            entity.Property(e => e.Cijena).HasColumnType("decimal(18,2)");

        });


        modelBuilder.Entity<Korpa>()
           .HasOne(m => m.Jelo)
           .WithMany()
           .HasForeignKey(m => m.JeloId);

        modelBuilder.Entity<Korpa>()
         .HasOne(m => m.Kategorija)
         .WithMany()
         .HasForeignKey(m => m.KategorijaId);

        modelBuilder.Entity<Status>().ToTable("Status");
        modelBuilder.Entity<Status>().HasData(
            new Status { Id = 1, Naziv = "Kreirana" },
            new Status { Id = 2, Naziv = "Prihvaćena" },
            new Status { Id = 3, Naziv = "U toku" },
            new Status { Id = 4, Naziv = "Završena" },
            new Status { Id = 5, Naziv = "Otkazana" }
        );


        modelBuilder.Entity<Uloge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Uloge__3214EC076C332449");

            entity.ToTable("Uloge");

            entity.Property(e => e.Naziv).HasMaxLength(100);
            entity.Property(e => e.Opis).HasMaxLength(100);
        });

        modelBuilder.Entity<Uplata>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Uplata__3214EC07C865A4BE");

            entity.Property(e => e.BrojTransakcije).HasMaxLength(1);
            entity.Property(e => e.DatumTransakcije).HasColumnType("datetime");
            entity.Property(e => e.Iznos).HasColumnType("money");

            entity.HasOne(d => d.Korisnik).WithMany(p => p.Uplata)
                .HasForeignKey(d => d.KorisnikId)
                .HasConstraintName("FK__Uplata__Korisnik__3E52440B");
        });
        //modelBuilder.Seed();
        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
