using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using BlazorApp2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BlazorApp2.Data;

public partial class Labb1DbContext : DbContext
{
    public Labb1DbContext()
    {
    }

    public Labb1DbContext(DbContextOptions<Labb1DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<GenresBook> GenresBooks { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<StoresEmployee> StoresEmployees { get; set; }

    public async Task<int> UpsertAuthor(Author author)
    {
        var exists = await Authors.FirstOrDefaultAsync(entry =>
            entry.Birthday == author.Birthday && entry.Lastname == author.Lastname &&
            entry.Firstname == author.Firstname); // ska det vara composite key kanske

        if (exists != null) return exists.AuthorId;

        Authors.Add(author);
        await SaveChangesAsync();
        
        return author.AuthorId;
    }
    public async Task<string> UpsertBook(Book book)
    {
        var exists = await Books.FirstOrDefaultAsync(entry =>
            entry.Isbn13 == book.Isbn13); 

        if (exists != null) return exists.Isbn13;

        Books.Add(book);
        await SaveChangesAsync();
        
        return book.Isbn13; // behövs inte men det blir consistent med den andra upsert
    }
    
    public async Task<List<dynamic>>? GetStores(params string[] fields)
    {
        var keys = GetForeignKeys(this, Stores);
        var queryResult = Stores;
        string joinCondition = "";
        
        foreach (IForeignKey key in keys)
        {
            string? foreignTableName = key.PrincipalEntityType.GetTableName();

            joinCondition += $"JOIN [{foreignTableName}] ON Stores.[{key.Properties.FirstOrDefault()?.Name}] = [{foreignTableName}].[{key.PrincipalKey.Properties[0].Name}] ";

        }
        var result = this.Stores.FromSqlRaw($"SELECT * FROM Stores {joinCondition}");
        
        return null;
        // var query = await
        //     (Stores.Join(this.Inventories,
        //         store => store,
        //         inventory => inventory.Store,
        //         (store, inventory) =>
        //             new ExpandoObject() { Store = store.StoreName, ISBN = inventory.Isbn, Quantity = inventory.Quantity }))
        //     .ToListAsync();
        //
        // return  query;
    }

    public static List<IForeignKey>? GetForeignKeys<T>(Labb1DbContext dbContext, DbSet<T> table) where T : class
    {
        var tableType = table.GetType().GetGenericArguments().First();

        return dbContext.Model.FindEntityType(tableType)?.GetForeignKeys().ToList();
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Addresse__091C2A1BCFB8833C");

            entity.HasOne(d => d.CityNavigation).WithMany(p => p.Addresses).HasConstraintName("FK__Addresses__City__398D8EEE");
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Authors__70DAFC1444095200");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Isbn13).HasName("PK__Books__3BF79E034F86D606");

            entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.Books).HasConstraintName("FK__Books__Author__267ABA7A");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.City1).HasName("PK__Cities__AEC4A06C88ABEF62");

            entity.HasOne(d => d.CountryNavigation).WithMany(p => p.Cities).HasConstraintName("FK__Cities__Country__36B12243");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Country1).HasName("PK__Countrie__067B3008D246EB59");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF15A085FF0");

            entity.HasOne(d => d.PositionNavigation).WithMany(p => p.Employees).HasConstraintName("FK__Employees__Posit__31EC6D26");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genres__0385055E9473767F");
        });

        modelBuilder.Entity<GenresBook>(entity =>
        {
            entity.HasOne(d => d.Genre).WithMany().HasConstraintName("FK__GenresBoo__Genre__2C3393D0");

            entity.HasOne(d => d.IsbnNavigation).WithMany().HasConstraintName("FK__GenresBook__ISBN__2B3F6F97");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => new { e.Isbn, e.StoreId }).HasName("PK__Inventor__47C519E52F7D86EC");

            entity.HasOne(d => d.IsbnNavigation).WithMany(p => p.Inventories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventory__ISBN__4316F928");

            entity.HasOne(d => d.Store).WithMany(p => p.Inventories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventory__Store__4222D4EF");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("PK__Position__60BB9A5991BB761C");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__Stores__3B82F0E1DB994C84");

            entity.HasOne(d => d.AddressNavigation).WithMany(p => p.Stores).HasConstraintName("FK__Stores__Address__3C69FB99");
        });

        modelBuilder.Entity<StoresEmployee>(entity =>
        {
            entity.HasOne(d => d.EmployeeNavigation).WithMany().HasConstraintName("FK__StoresEmp__Emplo__3E52440B");

            entity.HasOne(d => d.StoreNavigation).WithMany().HasConstraintName("FK__StoresEmp__Store__3F466844");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
