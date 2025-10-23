using Microsoft.EntityFrameworkCore;
using WebHostApplication.Models;
using WebHostApplication.Models.Clientes;
using WebHostApplication.Models.DetallesPedido;
//using WebHostApplication.Models.Detalles;
using WebHostApplication.Models.Division;
using WebHostApplication.Models.OrdenPedidos;
using WebHostApplication.Models.Pedidos;
using WebHostApplication.Models.RegistroZeta;
using WebHostApplication.Models.TuProyecto.Models;
using WebHostApplication.Models.Usuarios;
using WebHostApplication.Models.Vendedores;
using WebHostApplication.ViewModels.Utilidad;

namespace WebHostApplication.Data
{
    public class WebHostDbcontext: DbContext
    {
        public WebHostDbcontext(DbContextOptions<WebHostDbcontext> options):base(options)
        {
             
        }
        public DbSet<Usuario> Usuarios { get; set; }    
        public DbSet<Cajero> Cajeros { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Cliente> Cliente { get; set; } = default!;
        public DbSet<Division> Divisions { get; set; } = default!;
        public DbSet<Grupos> Grupos { get; set; } = default!;
        public DbSet<Productos> Productos { get; set; } = default!;
        public DbSet<RegistroZeta2> RegistroZeta2s { get; set; } = default!;
        public DbSet<ResolucionElectronica> ResolucionElectronicas { get; set; } = default!;
        public DbSet<Vendedor> Vendedores { get; set; } = default!;
        public DbSet<Vendedor2> Vendedor2 { get; set; } = default!;
        public DbSet<Cliente2> Clientes2 { get; set; } = default!;
        public DbSet<Detalle1> Detalle1 { get; set; } = default!;
        public DbSet<Pedido> Pedidos { get; set; } = default!;
        public DbSet<PedidoDetalles> PedidoDetalles { get; set; } = default!;
        public DbSet<Pedidos> Pedidos2 { get; set; } = default!;
        public DbSet<PedidosDetalles> PedidosDetalles2 { get; set; } = default!;
        public DbSet<Detalle2> Detalle2 { get; set; } = default!;
        public DbSet<Kardex> Kardex { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UtilidadFacturasDTO>().HasNoKey();

            modelBuilder.Entity<Vendedor>(entity =>
            {
                entity.ToTable("vendedor");

                entity.HasKey(e => e.IdVendedor)
                      .HasName("PRIMARY");

                entity.Property(e => e.IdVendedor)
                      .HasColumnName("idvendedor")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Id)
                      .HasColumnName("id")
                      .HasMaxLength(30)
                      .IsUnicode(false);

                entity.Property(e => e.Nombre)
                      .HasColumnName("nombre")
                      .HasMaxLength(30)
                      .IsUnicode(false);

                entity.Property(e => e.Apellido1)
                      .HasColumnName("apellido1")
                      .HasMaxLength(30)
                      .IsUnicode(false);

                entity.Property(e => e.Apellido2)
                      .HasColumnName("apellido2")
                      .HasMaxLength(30)
                      .IsUnicode(false);

                entity.Property(e => e.Dir)
                      .HasColumnName("dir")
                      .HasMaxLength(30)
                      .IsUnicode(false);

                entity.Property(e => e.Tel)
                      .HasColumnName("tel")
                      .HasMaxLength(30)
                      .IsUnicode(false);

                entity.Property(e => e.CreditoT)
                      .HasColumnName("creditot")
                      .HasColumnType("decimal(9,2)");

                entity.Property(e => e.AbonoT)
                      .HasColumnName("abonot")
                      .HasColumnType("decimal(9,2)");

                entity.Property(e => e.Condicion)
                      .HasColumnName("condicion");

                entity.Property(e => e.Comision)
                      .HasColumnName("comision")
                      .HasColumnType("decimal(9,2)");

                entity.Property(e => e.Porcentaje)
                      .HasColumnName("porcentaje")
                      .HasColumnType("decimal(9,2)");
            });
            modelBuilder.Entity<ResolucionElectronica>(entity =>
            {
                entity.ToTable("resoluciones_electronica");

                entity.HasKey(e => e.Id)
                      .HasName("PRIMARY");

                entity.Property(e => e.Tipo)
                      .HasColumnName("tipo");

                entity.Property(e => e.IdDian)
                      .HasColumnName("idDian");

                entity.Property(e => e.Inicio)
                      .HasColumnName("inicio");

                entity.Property(e => e.Final)
                      .HasColumnName("final");

                entity.Property(e => e.Consecutivo)
                      .HasColumnName("consecutivo");

                entity.Property(e => e.Estado)
                      .HasColumnName("estado")
                      .HasMaxLength(2)
                      .HasDefaultValue("A");

                entity.Property(e => e.FechaFinal)
                      .HasColumnName("fechaFinal")
                      .HasColumnType("date");

                entity.Property(e => e.Prefijo)
                      .HasColumnName("prefijo")
                      .HasMaxLength(10);

                entity.Property(e => e.Resolucion)
                      .HasColumnName("resolucion")
                      .HasMaxLength(20);

                entity.Property(e => e.Fecha)
                      .HasColumnName("fecha")
                      .HasColumnType("date");

                entity.Property(e => e.IdResolDian)
                      .HasColumnName("idResolDian");
            });
            modelBuilder.Entity<RegistroZeta2>(entity =>
            {
                entity.ToTable("registrozeta2");

                entity.HasKey(e => e.Id)
                      .HasName("PRIMARY");

                entity.Property(e => e.Id)
                      .HasColumnName("id")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Z2)
                      .HasColumnName("z2")
                      .IsRequired();

                entity.Property(e => e.FechaHora)
                      .HasColumnName("fecha_hora")
                      .HasColumnType("datetime")
                      .IsRequired();

                entity.Property(e => e.Maq)
                      .HasColumnName("maq")
                      .IsRequired();
            });
            modelBuilder.Entity<Productos>(entity =>
            {
                entity.ToTable("productos");

                entity.HasKey(e => e.Id)
                      .HasName("PRIMARY");

                entity.Property(e => e.Id)
                      .HasColumnName("id")
                      .HasMaxLength(30)
                      .IsRequired();

                entity.Property(e => e.Descripcion)
                      .HasColumnName("descripcion")
                      .HasMaxLength(60);

                entity.Property(e => e.Precio1)
                      .HasColumnName("precio1")
                      .HasColumnType("decimal(9,2)");

                entity.Property(e => e.Precio2)
                      .HasColumnName("precio2")
                      .HasColumnType("decimal(9,2)");

                entity.Property(e => e.Precio3)
                      .HasColumnName("precio3")
                      .HasColumnType("decimal(9,2)");

                entity.Property(e => e.Nfrac)
                      .HasColumnName("nfrac")
                      .HasColumnType("decimal(9,2)");

                entity.Property(e => e.Existencia)
                      .HasColumnName("existencia")
                      .HasColumnType("decimal(9,2)");

                entity.Property(e => e.Grupo)
                      .HasColumnName("grupo")
                      .HasMaxLength(30);

                entity.Property(e => e.Iva)
                      .HasColumnName("iva")
                      .HasColumnType("decimal(9,2)");

                entity.Property(e => e.Costo)
                      .HasColumnName("costo")
                      .HasColumnType("decimal(9,2)");

                entity.Property(e => e.Max)
                      .HasColumnName("max")
                      .HasMaxLength(10);

                entity.Property(e => e.Min)
                      .HasColumnName("min")
                      .HasMaxLength(10);

                entity.Property(e => e.Uni)
                      .HasColumnName("Uni")
                      .HasMaxLength(3);

                entity.Property(e => e.Codigo2)
                      .HasColumnName("codigo2")
                      .HasMaxLength(15);

                entity.Property(e => e.Rf)
                      .HasColumnName("rf")
                      .HasColumnType("text");
            });
            modelBuilder.Entity<Cajero>(entity =>
            {
                entity.ToTable("Cajero"); 

                entity.HasKey(e => e.IdCajero);

                entity.Property(e => e.IdCajero)
                    .HasColumnName("idcajero")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PrimerApellido)
                    .HasColumnName("primerapellido")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SegundoApellido)
                    .HasColumnName("segundoapellido")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.dato)
                    .HasColumnName("dato")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                
            });
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("Categoria"); 

                entity.HasKey(e => e.IdCategoria);

                entity.Property(e => e.IdCategoria)
                    .HasColumnName("IdCategoria")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ButtonName)
                    .HasColumnName("ButtonName")
                    .HasMaxLength(100)
                    .IsUnicode(true)  // porque la collation es utf8mb3_general_ci
                    .IsRequired();

                entity.Property(e => e.NombreCategoria)
                    .HasColumnName("NombreCategoria")
                    .HasMaxLength(255)
                    .IsUnicode(true)
                    .IsRequired();

                entity.Property(e => e.ImagenRuta)
                    .HasColumnName("ImagenRuta")
                    .HasMaxLength(255)
                    .IsUnicode(true);

                entity.Property(e => e.Fuente)
                    .HasColumnName("Fuente")
                    .HasMaxLength(100)
                    .IsUnicode(true);
            });

            modelBuilder.Entity<Cliente2>(entity =>
            {
                entity.HasKey(e => e.IdCliente2);

                entity.ToTable("clientes2");

                entity.Property(e => e.Id)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Apellido1)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Apellido2)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Dir)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Tel)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Porcentaje)
                    .HasColumnType("decimal(9,2)");

                entity.Property(e => e.Condicion);
                
            });
            modelBuilder.Entity<Vendedor2>(entity =>
            {
                entity.HasKey(e => e.IdVendedor);

                entity.ToTable("vendedor2");

                entity.Property(e => e.Id)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Apellido1)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Apellido2)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Dir)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Tel)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CreditoT)
                    .HasColumnType("decimal(9,2)");

                entity.Property(e => e.AbonoT)
                    .HasColumnType("decimal(9,2)");

                entity.Property(e => e.Comision)
                    .HasColumnType("decimal(9,2)");

                entity.Property(e => e.Porcentaje)
                    .HasColumnType("decimal(9,2)");

                //// 🔗 Relación con facturas (Detalle2)
                //entity.HasMany(v => v.Facturas)
                //      .WithOne(f => f.Vendedor)
                //      .HasForeignKey(f => f.IdVendedor)
                //      .OnDelete(DeleteBehavior.Restrict)
                //      .HasConstraintName("detalle2_ibfk_3");
            });
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("clientes");

                entity.HasKey(e => e.IdCliente);

                entity.Property(e => e.IdCliente)
                    .HasColumnName("idcliente")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Apellido1)
                    .HasColumnName("apellido1")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Apellido2)
                    .HasColumnName("apellido2")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Dir)
                    .HasColumnName("dir")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Tel)
                    .HasColumnName("tel")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasColumnName("correo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasColumnName("tipo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Porcentaje)
                    .HasColumnName("porcentaje")
                    .HasColumnType("decimal(9,2)");

                entity.Property(e => e.Condicion)
                    .HasColumnName("condicion");

                entity.Property(e => e.DocId)
                    .HasColumnName("doc_id")
                    .HasDefaultValue(3);

                entity.Property(e => e.TipoTercero)
                    .HasColumnName("tipotercero")
                    .HasDefaultValue(2);

                entity.Property(e => e.Maximo)
                    .HasColumnName("MAX")
                    .HasColumnType("decimal(9,2)");
            });
            modelBuilder.Entity<Grupos>(entity =>
            {
                entity.ToTable("grupos");

                entity.HasKey(e => e.IdGrupo)
                      .HasName("PRIMARY");

                entity.Property(e => e.IdGrupo)
                      .HasColumnName("idGrupo");

                entity.Property(e => e.Grupo)
                      .IsRequired()
                      .HasColumnName("Grupo")
                      .HasMaxLength(60)
                      .IsUnicode(false);

                entity.HasIndex(e => e.Grupo)
                      .IsUnique()
                      .HasDatabaseName("Grupo");
            });
            modelBuilder.Entity<Division>(entity =>
            {
                entity.ToTable("division");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Boton)
                    .HasColumnName("boton")
                    .HasMaxLength(50)
                    .IsUnicode(true);

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(100)
                    .IsUnicode(true);

                entity.Property(e => e.RutaImagen)
                    .HasColumnName("RutaImagen")
                    .HasMaxLength(255)
                    .IsUnicode(true);
            });
            // Configuración de Detalle1
            modelBuilder.Entity<Detalle1>(entity =>
            {
                entity.HasKey(e => e.Ind);

                // Relación con Detalle2 (Factura)
                entity.HasOne(d => d.Detalle2)
                    .WithMany(p => p.Detalles)   // 👈 en Detalle2 ya tienes List<Detalle1>
                    .HasForeignKey(d => d.NumeroFactura)
                     .HasPrincipalKey(v => v.IdVendedor)
                    .HasConstraintName("FK_Detalle1_Detalle2");

                // Relación con Vendedor2
                  entity.HasOne(d => d.Vendedor2)
                    .WithMany(v => v.Ventas)
                    .HasForeignKey(d => d.IdVendedor2)
                    .HasPrincipalKey(v => v.IdVendedor)  // 👈 aquí la magia
                    .OnDelete(DeleteBehavior.Restrict);
                
            });

            // =============================
            // 🔹 Tabla detalle2 (Factura)
            // =============================
            modelBuilder.Entity<Detalle2>(entity =>
            {
                entity.ToTable("detalle2");

                entity.HasKey(e => e.NumeroFactura);

                entity.Property(e => e.NumeroFactura)
                      .HasColumnName("numeroFactura");

                entity.Property(e => e.Fecha)
                      .HasColumnName("fecha")
                      .HasColumnType("date");

                entity.Property(e => e.IdCliente)
                      .HasColumnName("idcliente");

                entity.Property(e => e.IdCliente2)
                      .HasColumnName("idcliente2");

                entity.Property(e => e.IdVendedor)
                      .HasColumnName("idvendedor");

                entity.Property(e => e.IdCajero)
                      .HasColumnName("idcajero");

                entity.Property(e => e.Servicio1).HasColumnName("servicio1");
                entity.Property(e => e.Servicio2).HasColumnName("servicio2");
                entity.Property(e => e.Servicio3).HasColumnName("servicio3");

                entity.Property(e => e.Subtotal).HasColumnName("subtotal");
                entity.Property(e => e.Subtotal2).HasColumnName("subtotal2");
                entity.Property(e => e.Subtotal3).HasColumnName("subtotal3");

                entity.Property(e => e.Dinero).HasColumnName("dinero");
                entity.Property(e => e.TotalDescuento).HasColumnName("totaldescuento");
                entity.Property(e => e.Cambio).HasColumnName("cambio");
                entity.Property(e => e.Efectivo).HasColumnName("efectivo");
                entity.Property(e => e.Tcredito).HasColumnName("tcredito");
                entity.Property(e => e.Tdebito).HasColumnName("tdebito");
                entity.Property(e => e.Credito).HasColumnName("credito");
                entity.Property(e => e.Transferencia).HasColumnName("transferencia");
                entity.Property(e => e.TT).HasColumnName("tt");
                entity.Property(e => e.Gratis).HasColumnName("gratis");
                entity.Property(e => e.Z2).HasColumnName("z2");

                entity.Property(e => e.Observacion)
                      .HasColumnName("observacion")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.Electronica)
                      .HasColumnName("electronica");
            });

            // =============================
            // 🔹 Tabla detalle1 (Detalle de Factura)
            // =============================
            modelBuilder.Entity<Detalle1>(entity =>
            {
                entity.ToTable("detalle1");

                entity.HasKey(e => e.Ind);

                entity.Property(e => e.Ind)
                      .HasColumnName("id");

                entity.Property(e => e.NumeroFactura)
                      .HasColumnName("numerofactura");

                entity.Property(e => e.CodigoFijo)
                      .HasColumnName("codigofijo")
                      .HasColumnType("varchar(50)");

                entity.Property(e => e.Descripcion)
                      .HasColumnName("descripcion")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.Precio)
                      .HasColumnName("precio")
                      .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Can)
                      .HasColumnName("can")
                      .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Subtotal)
                      .HasColumnName("subtotal")
                      .HasColumnType("decimal(18,2)");

                entity.Property(e => e.IdVendedor2)
                      .HasColumnName("idvendedor2");

                // 🔑 Relación: detalle1 → detalle2
                entity.HasOne(d => d.Detalle2)
                      .WithMany(p => p.Detalles)
                      .HasForeignKey(d => d.NumeroFactura)
                      .HasPrincipalKey(p => p.NumeroFactura)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_detalle1_detalle2");
            });

            // Configuración de Vendedor2
            modelBuilder.Entity<Vendedor2>(entity =>
            {
                entity.HasKey(e => e.IdVendedor)
      .HasName("PRIMARY");

                // 🔹 Propiedades
                entity.Property(e => e.IdVendedor)
                    .HasColumnName("idvendedor")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Apellido1)
                    .HasColumnName("apellido1")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Apellido2)
                    .HasColumnName("apellido2")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Dir)
                    .HasColumnName("dir")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Tel)
                    .HasColumnName("tel")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CreditoT)
                    .HasColumnName("creditot")
                    .HasColumnType("decimal(9,2)");

                entity.Property(e => e.AbonoT)
                    .HasColumnName("abonot")
                    .HasColumnType("decimal(9,2)");

                entity.Property(e => e.Condicion)
                    .HasColumnName("condicion");

                entity.Property(e => e.Comision)
                    .HasColumnName("comision")
                    .HasColumnType("decimal(9,2)");

                entity.Property(e => e.Porcentaje)
                    .HasColumnName("porcentaje")
                    .HasColumnType("decimal(9,2)");

                // 🔗 Relación con Detalle1 (uno a muchos)
                entity.HasMany(e => e.Ventas)
                    .WithOne(d => d.Vendedor2)
                    .HasForeignKey(d => d.IdVendedor2)
                    .HasConstraintName("FK_Detalle1_Vendedor2"); // asumiendo que se llama IdVendedor
            });
            // 🔧 Configuración de la tabla Kardex
            modelBuilder.Entity<Kardex>(entity =>
            {
                entity.ToTable("kardex");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Fecha)
                      .HasColumnName("fecha")
                      .HasColumnType("datetime");

                entity.Property(e => e.TipoMovimiento)
                      .HasColumnName("tipo_movimiento")
                      .HasMaxLength(20);

                entity.Property(e => e.ProductoId)
                      .HasColumnName("producto_id")
                      .HasMaxLength(50);

                entity.Property(e => e.Descripcion)
                      .HasColumnName("descripcion")
                      .HasMaxLength(255);

                entity.Property(e => e.Cantidad)
                      .HasColumnName("cantidad")
                      .HasColumnType("decimal(10,2)");

                entity.Property(e => e.PrecioVenta)
                      .HasColumnName("precio_venta")
                      .HasColumnType("decimal(10,2)");

                entity.Property(e => e.PrecioCosto)
                      .HasColumnName("precio_costo")
                      .HasColumnType("decimal(10,2)");

                entity.Property(e => e.Utilidad)
                      .HasColumnName("utilidad")
                      .HasColumnType("decimal(10,2)");

                entity.Property(e => e.ExistenciaAntes)
                      .HasColumnName("existencia_antes")
                      .HasColumnType("decimal(10,2)");

                entity.Property(e => e.ExistenciaDespues)
                      .HasColumnName("existencia_despues")
                      .HasColumnType("decimal(10,2)");

                entity.Property(e => e.ReferenciaDocumento)
                      .HasColumnName("referencia_documento")
                      .HasMaxLength(50);

                entity.Property(e => e.Usuario)
                      .HasColumnName("usuario")
                      .HasMaxLength(100);

                entity.Property(e => e.Observacion)
                      .HasColumnName("observacion")
                      .HasColumnType("text");
            });
        }
    }

}
        

