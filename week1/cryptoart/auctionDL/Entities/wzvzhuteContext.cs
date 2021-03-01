using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace auctionDL
{
    public partial class wzvzhuteContext : DbContext
    {
        public wzvzhuteContext()
        {
        }

        public wzvzhuteContext(DbContextOptions<wzvzhuteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Art> Arts { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Artistcollection> Artistcollections { get; set; }
        public virtual DbSet<Auction> Auctions { get; set; }
        public virtual DbSet<Bid> Bids { get; set; }
        public virtual DbSet<Blacklist> Blacklists { get; set; }
        public virtual DbSet<Collector> Collectors { get; set; }
        public virtual DbSet<Collectorsinventory> Collectorsinventories { get; set; }
        public virtual DbSet<PgStatStatement> PgStatStatements { get; set; }
        public virtual DbSet<Seller> Sellers { get; set; }
        public virtual DbSet<Sellersinventory> Sellersinventories { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("btree_gin")
                .HasPostgresExtension("btree_gist")
                .HasPostgresExtension("citext")
                .HasPostgresExtension("cube")
                .HasPostgresExtension("dblink")
                .HasPostgresExtension("dict_int")
                .HasPostgresExtension("dict_xsyn")
                .HasPostgresExtension("earthdistance")
                .HasPostgresExtension("fuzzystrmatch")
                .HasPostgresExtension("hstore")
                .HasPostgresExtension("intarray")
                .HasPostgresExtension("ltree")
                .HasPostgresExtension("pg_stat_statements")
                .HasPostgresExtension("pg_trgm")
                .HasPostgresExtension("pgcrypto")
                .HasPostgresExtension("pgrowlocks")
                .HasPostgresExtension("pgstattuple")
                .HasPostgresExtension("tablefunc")
                .HasPostgresExtension("unaccent")
                .HasPostgresExtension("uuid-ossp")
                .HasPostgresExtension("xml2")
                .HasAnnotation("Relational:Collation", "en_US.UTF-8");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("accounts");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Balance)
                    .HasPrecision(15, 2)
                    .HasColumnName("balance");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Art>(entity =>
            {
                entity.ToTable("art");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Artistcommentary).HasColumnName("artistcommentary");

                entity.Property(e => e.Artpiece).HasColumnName("artpiece");

                entity.Property(e => e.Buynowprice)
                    .HasColumnType("money")
                    .HasColumnName("buynowprice");

                entity.Property(e => e.Currentvalue)
                    .HasColumnType("money")
                    .HasColumnName("currentvalue");

                entity.Property(e => e.Description)
                    .HasColumnType("character varying")
                    .HasColumnName("description");

                entity.Property(e => e.Location)
                    .HasColumnType("character varying")
                    .HasColumnName("location");

                entity.Property(e => e.Maxseries).HasColumnName("maxseries");

                entity.Property(e => e.Name)
                    .HasColumnType("character varying")
                    .HasColumnName("name");

                entity.Property(e => e.Seriesnumber).HasColumnName("seriesnumber");

                entity.Property(e => e.Thumbnail).HasColumnName("thumbnail");
            });

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.ToTable("artist");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Artiststatement).HasColumnName("artiststatement");

                entity.Property(e => e.Biography).HasColumnName("biography");

                entity.Property(e => e.Country)
                    .HasColumnType("character varying")
                    .HasColumnName("country");

                entity.Property(e => e.Digitalsignature).HasColumnName("digitalsignature");

                entity.Property(e => e.Location)
                    .HasColumnType("character varying")
                    .HasColumnName("location");

                entity.Property(e => e.Name)
                    .HasColumnType("character varying")
                    .HasColumnName("name");

                entity.Property(e => e.Signature)
                    .HasColumnType("character varying")
                    .HasColumnName("signature");
            });

            modelBuilder.Entity<Artistcollection>(entity =>
            {
                entity.HasKey(e => new { e.Artistid, e.Artid })
                    .HasName("pk_artistcollection");

                entity.ToTable("artistcollection");

                entity.Property(e => e.Artistid).HasColumnName("artistid");

                entity.Property(e => e.Artid).HasColumnName("artid");

                entity.HasOne(d => d.Art)
                    .WithMany(p => p.Artistcollections)
                    .HasForeignKey(d => d.Artid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_artistcollection_art");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.Artistcollections)
                    .HasForeignKey(d => d.Artistid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_artistcollection_artists");
            });

            modelBuilder.Entity<Auction>(entity =>
            {
                entity.ToTable("auction");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Artid).HasColumnName("artid");

                entity.Property(e => e.Closingdate).HasColumnName("closingdate");

                entity.Property(e => e.Minimumamount)
                    .HasColumnType("money")
                    .HasColumnName("minimumamount");

                entity.Property(e => e.Sellerid).HasColumnName("sellerid");

                entity.HasOne(d => d.Art)
                    .WithMany(p => p.Auctions)
                    .HasForeignKey(d => d.Artid)
                    .HasConstraintName("fk_auction_art");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Auctions)
                    .HasForeignKey(d => d.Sellerid)
                    .HasConstraintName("fk_auction_seller");
            });

            modelBuilder.Entity<Bid>(entity =>
            {
                entity.ToTable("bids");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnType("money")
                    .HasColumnName("amount");

                entity.Property(e => e.Auctionid).HasColumnName("auctionid");

                entity.Property(e => e.Collectorid).HasColumnName("collectorid");

                entity.Property(e => e.Timeofbid).HasColumnName("timeofbid");

                entity.HasOne(d => d.Auction)
                    .WithMany(p => p.Bids)
                    .HasForeignKey(d => d.Auctionid)
                    .HasConstraintName("fk_bid_auction");

                entity.HasOne(d => d.Collector)
                    .WithMany(p => p.Bids)
                    .HasForeignKey(d => d.Collectorid)
                    .HasConstraintName("fk_bid_collector");
            });

            modelBuilder.Entity<Blacklist>(entity =>
            {
                entity.ToTable("blacklist");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Artid).HasColumnName("artid");

                entity.Property(e => e.Location)
                    .HasColumnType("character varying")
                    .HasColumnName("location");

                entity.HasOne(d => d.Art)
                    .WithMany(p => p.Blacklists)
                    .HasForeignKey(d => d.Artid)
                    .HasConstraintName("fk_blacklist_art");
            });

            modelBuilder.Entity<Collector>(entity =>
            {
                entity.ToTable("collectors");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Location)
                    .HasColumnType("character varying")
                    .HasColumnName("location");

                entity.Property(e => e.Name)
                    .HasColumnType("character varying")
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Collectorsinventory>(entity =>
            {
                entity.HasKey(e => new { e.Collectorid, e.Artid })
                    .HasName("pk_collectorsinventory");

                entity.ToTable("collectorsinventory");

                entity.Property(e => e.Collectorid).HasColumnName("collectorid");

                entity.Property(e => e.Artid).HasColumnName("artid");

                entity.HasOne(d => d.Art)
                    .WithMany(p => p.Collectorsinventories)
                    .HasForeignKey(d => d.Artid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_collectorsinventory_art");

                entity.HasOne(d => d.Collector)
                    .WithMany(p => p.Collectorsinventories)
                    .HasForeignKey(d => d.Collectorid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_collectorsinventory_collector");
            });

            modelBuilder.Entity<PgStatStatement>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("pg_stat_statements");

                entity.Property(e => e.BlkReadTime).HasColumnName("blk_read_time");

                entity.Property(e => e.BlkWriteTime).HasColumnName("blk_write_time");

                entity.Property(e => e.Calls).HasColumnName("calls");

                entity.Property(e => e.Dbid)
                    .HasColumnType("oid")
                    .HasColumnName("dbid");

                entity.Property(e => e.LocalBlksDirtied).HasColumnName("local_blks_dirtied");

                entity.Property(e => e.LocalBlksHit).HasColumnName("local_blks_hit");

                entity.Property(e => e.LocalBlksRead).HasColumnName("local_blks_read");

                entity.Property(e => e.LocalBlksWritten).HasColumnName("local_blks_written");

                entity.Property(e => e.MaxTime).HasColumnName("max_time");

                entity.Property(e => e.MeanTime).HasColumnName("mean_time");

                entity.Property(e => e.MinTime).HasColumnName("min_time");

                entity.Property(e => e.Query).HasColumnName("query");

                entity.Property(e => e.Queryid).HasColumnName("queryid");

                entity.Property(e => e.Rows).HasColumnName("rows");

                entity.Property(e => e.SharedBlksDirtied).HasColumnName("shared_blks_dirtied");

                entity.Property(e => e.SharedBlksHit).HasColumnName("shared_blks_hit");

                entity.Property(e => e.SharedBlksRead).HasColumnName("shared_blks_read");

                entity.Property(e => e.SharedBlksWritten).HasColumnName("shared_blks_written");

                entity.Property(e => e.StddevTime).HasColumnName("stddev_time");

                entity.Property(e => e.TempBlksRead).HasColumnName("temp_blks_read");

                entity.Property(e => e.TempBlksWritten).HasColumnName("temp_blks_written");

                entity.Property(e => e.TotalTime).HasColumnName("total_time");

                entity.Property(e => e.Userid)
                    .HasColumnType("oid")
                    .HasColumnName("userid");
            });

            modelBuilder.Entity<Seller>(entity =>
            {
                entity.ToTable("seller");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnType("character varying")
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Sellersinventory>(entity =>
            {
                entity.HasKey(e => new { e.Sellerid, e.Artid })
                    .HasName("pk_sellersinventory");

                entity.ToTable("sellersinventory");

                entity.Property(e => e.Sellerid).HasColumnName("sellerid");

                entity.Property(e => e.Artid).HasColumnName("artid");

                entity.HasOne(d => d.Art)
                    .WithMany(p => p.Sellersinventories)
                    .HasForeignKey(d => d.Artid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_sellersinventory_art");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Sellersinventories)
                    .HasForeignKey(d => d.Sellerid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_sellersinventory_seller");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
