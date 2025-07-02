using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using CDS_Models;
using CDS_Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDS_DAL
{
    public partial class LogistContext : DbContext
    {
        public LogistContext()
        {
        }

        public LogistContext(DbContextOptions<LogistContext> options)
            : base(options)
        {
        }

        public  DbSet<OrdenPedido> OrdenPedido { get; set; }
        public  DbSet<Moneda> Monedas { get; set; }
        public  DbSet<Cliente> Clientes { get; set; }
        public  DbSet<Vendedor> Vendedores { get; set; }
        public  DbSet<TipoDocumento> TiposDocumento { get; set; }
        public  DbSet<Marca> Marcas { get; set; }
        public  DbSet<SubTiposNegocio> SubTiposNegocio { get; set; }
        public  DbSet<TiposNegocio> TiposNegocio { get; set; }
        public  DbSet<SubSubTiposNegocio> SubSubTiposNegocio { get; set; }
        public  DbSet<StatusOp> StatusOp { get; set; }
        public  DbSet<FormaPago> FormaPago { get; set; }
        public  DbSet<CatFormaPago> CatFormaPago { get; set; }
        public  DbSet<UnidadMedida> UnidadesMedida { get; set; }
        public  DbSet<ProdUm> ProdUm { get; set; }
        public  DbSet<TcUsd> TcUsd { get; set; }
        public  DbSet<Producto> Productos { get; set; }
        public  DbSet<Servicio> Servicios { get; set; }
        public  DbSet<Proyecto> Proyectos { get; set; }
        public  DbSet<Clase> Clases { get; set; }
        public  DbSet<SubClase> SubClases { get; set; }
        public  DbSet<SubSubClase> SubSubClases { get; set; }
        public  DbSet<CCosto> CCostos { get; set; }
        public  DbSet<ScCosto> ScCostos { get; set; }
        public  DbSet<SscCosto> SscCostos { get; set; }
        public  DbSet<OrdenPedidoDetalle> OrdenesPedidoDetalle { get; set; }
        public  DbSet<Login> Logins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var envConn = Environment.GetEnvironmentVariable("RAILWAY_DATABASE_URL");
                if (!string.IsNullOrEmpty(envConn))
                {
                    optionsBuilder.UseSqlServer(envConn);
                }
                else
                {
                    IConfigurationRoot configuration = new ConfigurationBuilder()
                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile("appsettings.json")
                        .Build();
                    optionsBuilder.UseSqlServer(configuration.GetConnectionString("BD_LOGISTICA_LOCAL"));
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProdUm>()
                .HasKey(pu => new { pu.IdProd, pu.IdUm });

            modelBuilder.Entity<OrdenPedido>(entity =>
            {
                entity.HasKey(e => e.IdOpci);

                entity.Property(e => e.IdOpci).HasColumnName("ID_OPCI");
                entity.Property(e => e.IdFp).HasColumnName("ID_FP");
                entity.Property(e => e.IdCliente).HasColumnName("ID_Cliente");
                entity.Property(e => e.IdVendedor).HasColumnName("ID_Vendedor");
                entity.Property(e => e.IdOpd).HasColumnName("ID_OPD");
                entity.Property(e => e.FecRecepcion).HasColumnName("FecRecepcion");
                entity.Property(e => e.FecInicio).HasColumnName("FecInicio");
                entity.Property(e => e.FecProcVi).HasColumnName("FecProcVI");
                entity.Property(e => e.RazonSocialCliente).HasColumnName("RazonSocialCliente");
                entity.Property(e => e.NumOp).HasColumnName("NumOP");
                entity.Property(e => e.IdMda).HasColumnName("ID_Mda");
                entity.Property(e => e.TotalSinIgv).HasColumnName("TotalSinIGV");
                entity.Property(e => e.NumRefCliente).HasColumnName("NumRefCliente");
                entity.Property(e => e.ClienteFinal).HasColumnName("ClienteFinal");
                entity.Property(e => e.ClienteProveedor).HasColumnName("ClienteProveedor");
                entity.Property(e => e.Vendedor1).HasColumnName("Vendedor1");
                entity.Property(e => e.Vendedor2).HasColumnName("Vendedor2");
                entity.Property(e => e.Lider).HasColumnName("Lider");
                entity.Property(e => e.UbrutaCoti).HasColumnName("UbrutaCoti");
                entity.Property(e => e.ComisionCompartida).HasColumnName("ComisionCompartida");
                entity.Property(e => e.Estado).HasColumnName("Estado");

                entity.HasOne(d => d.FormaPago)
                    .WithMany(p => p.OrdenPedido)
                    .HasForeignKey(d => d.IdFp)
                    .HasConstraintName("FK_ORDENPEDIDO_FORMAPAGO");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.OrdenPedido)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK_ORDENPEDIDO_CLIENTE");

                entity.HasOne(d => d.Vendedor)
                    .WithMany(p => p.OrdenPedido)
                    .HasForeignKey(d => d.IdVendedor)
                    .HasConstraintName("FK_ORDENPEDIDO_VENDEDORES");

                entity.HasOne(d => d.Moneda)
                    .WithMany(p => p.OrdenPedido)
                    .HasForeignKey(d => d.IdMda)
                    .HasConstraintName("FK_ORDENPEDIDO_MONEDAS");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("CLIENTE");
                entity.HasKey(e => e.IdCliente);

                entity.Property(e => e.IdCliente).HasColumnName("ID_Cliente");
                entity.Property(e => e.IdTdi).HasColumnName("ID_TDI");
                entity.Property(e => e.RazonSocial).HasColumnName("RazonSocial");
                entity.Property(e => e.CorreoCliente).HasColumnName("CorreoCliente");
                entity.Property(e => e.NumDocumento).HasColumnName("NumDocumento");
                entity.Property(e => e.TelefonoCliente).HasColumnName("TelefonoCliente");
                entity.Property(e => e.DireccionCliente).HasColumnName("DireccionCliente");
                entity.Property(e => e.IbCltPrv).HasColumnName("IB_CltPrv");
                entity.Property(e => e.IbCltFinal).HasColumnName("IB_CltFinal");

                entity.HasOne(d => d.TipoDocumento)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.IdTdi)
                    .HasConstraintName("FK_CLIENTE_TIPODOCUMENTO");
            });

            modelBuilder.Entity<Vendedor>(entity =>
            {
                entity.HasKey(e => e.IdVendedor);

                entity.Property(e => e.IdVendedor).HasColumnName("ID_Vendedor");
                entity.Property(e => e.IdTdi).HasColumnName("ID_TDI");
                entity.Property(e => e.NumDocVendedor).HasColumnName("NumDoc_Vendedor");
                entity.Property(e => e.NombreVendedor).HasColumnName("Nombre_Vendedor");
                entity.Property(e => e.IbLider).HasColumnName("IB_Lider");
                entity.Property(e => e.Estado).HasColumnName("Estado");

                entity.HasOne(d => d.TipoDocumento)
                    .WithMany(p => p.Vendedor)
                    .HasForeignKey(d => d.IdTdi)
                    .HasConstraintName("FK_VENDEDORES_TIPODOCUMENTO");
            });

            modelBuilder.Entity<SubTiposNegocio>(static entity =>
            {
                entity.HasKey(e => e.IdStn);

                entity.Property(e => e.IdStn).HasColumnName("ID_STN");
                entity.Property(e => e.IdTn).HasColumnName("ID_TN");
                entity.Property(e => e.DescripcionStn).HasColumnName("DescripcionSTN");
                entity.Property(e => e.NomCorto).HasColumnName("NomCorto");
                entity.Property(e => e.Estado).HasColumnName("Estado");

                entity.HasOne(d => d.TiposNegocio)
                    .WithMany(p => p.SubTiposNegocio)
                    .HasForeignKey(d => d.IdTn)
                    .HasConstraintName("FK_STNEGOCIO_TIPONEGOCIO");
            });

            modelBuilder.Entity<SubSubTiposNegocio>(entity =>
            {
                entity.HasKey(e => e.IdSstn);

                entity.Property(e => e.IdSstn).HasColumnName("ID_SSTN");
                entity.Property(e => e.IdStn).HasColumnName("ID_STN");
                entity.Property(e => e.DescripcionSstn).HasColumnName("DescripcionSSTN");
                entity.Property(e => e.NomCorto).HasColumnName("NomCorto");
                entity.Property(e => e.Estado).HasColumnName("Estado");

                entity.HasOne(d => d.SubTiposNegocio)
                    .WithMany(p => p.SubSubTiposNegocio)
                    .HasForeignKey(d => d.IdStn)
                    .HasConstraintName("FK_SSTN_STNEGOCIO");
            });

            modelBuilder.Entity<FormaPago>(entity =>
            {
                entity.HasKey(e => e.IdFp);

                entity.Property(e => e.IdFp).HasColumnName("ID_FP");
                entity.Property(e => e.IdCfp).HasColumnName("ID_CFP");
                entity.Property(e => e.DescripcionFp).HasColumnName("DescripcionFP");
                entity.Property(e => e.NomCorto).HasColumnName("NomCorto");
                entity.Property(e => e.Estado).HasColumnName("Estado");

                entity.HasOne(d => d.CatFormaPago)
                    .WithMany(p => p.FormaPago)
                    .HasForeignKey(d => d.IdCfp)
                    .HasConstraintName("FK_FORMAPAGO_CFORMAPAGO");
            });

            modelBuilder.Entity<ProdUm>(entity =>
            {
                entity.HasKey(e => new { e.IdProd, e.IdUm });

                entity.Property(e => e.IdProd).HasColumnName("ID_Prod");
                entity.Property(e => e.IdUm).HasColumnName("ID_UM");
                entity.Property(e => e.Descripcion).HasColumnName("Descripcion");
                entity.Property(e => e.Estado).HasColumnName("Estado");

                entity.HasOne(d => d.Productos)
                    .WithMany(p => p.ProdUm)
                    .HasForeignKey(d => d.IdProd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRODUM_PRODUCTOS");

                entity.HasOne(d => d.UnidadesMedida)
                    .WithMany(p => p.ProdUm)
                    .HasForeignKey(d => d.IdUm)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRODUM_UNIDADMEDIDA");
            });

            modelBuilder.Entity<TcUsd>(entity =>
            {
                entity.HasKey(e => e.IdTc);

                entity.Property(e => e.IdTc).HasColumnName("ID_TC");
                entity.Property(e => e.IdMda).HasColumnName("ID_Mda");
                entity.Property(e => e.FechaTc).HasColumnName("FechaTC");
                entity.Property(e => e.TipCam).HasColumnName("TipCam");

                entity.HasOne(d => d.Moneda)
                    .WithMany(p => p.TcUsd)
                    .HasForeignKey(d => d.IdMda)
                    .HasConstraintName("FK_TCUSD_MONEDAS");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProd);

                entity.Property(e => e.IdProd).HasColumnName("ID_Prod");
                entity.Property(e => e.IdMarca).HasColumnName("ID_Marca");
                entity.Property(e => e.CodComercial1).HasColumnName("Cod_Comercial1");
                entity.Property(e => e.CodComercial2).HasColumnName("Cod_Comercial2");
                entity.Property(e => e.CodComercial3).HasColumnName("Cod_Comercial3");
                entity.Property(e => e.Descripcion).HasColumnName("Descripcion");
                entity.Property(e => e.NomCorto).HasColumnName("NomCorto");
                entity.Property(e => e.IdClase).HasColumnName("ID_Clase");
                entity.Property(e => e.IdSClase).HasColumnName("ID_SClase");
                entity.Property(e => e.IdSSClase).HasColumnName("ID_SSClase");
                entity.Property(e => e.IdCc).HasColumnName("ID_CC").HasMaxLength(50);
                entity.Property(e => e.IdScc).HasColumnName("ID_SCC").HasMaxLength(50);
                entity.Property(e => e.IdSscc).HasColumnName("ID_SSCC").HasMaxLength(50);
                entity.Property(e => e.Estado).HasColumnName("Estado");

                entity.HasOne(d => d.Marca)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdMarca)
                    .HasConstraintName("FK_PRODUCTOS_MARCA");

                entity.HasOne(d => d.Clase)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdClase)
                    .HasConstraintName("FK_PRODUCTOS_CLASE");

                entity.HasOne(d => d.SubClase)
                    .WithMany(p => p.ProductosSubClase)
                    .HasForeignKey(d => d.IdSClase)
                    .HasConstraintName("FK_PRODUCTOS_SUBCLASE");

                entity.HasOne(d => d.SubSubClase)
                    .WithMany(p => p.ProductosSubSubClase)
                    .HasForeignKey(d => d.IdSSClase)
                    .HasConstraintName("FK_PRODUCTOS_SSCLASE");

                entity.HasOne(d => d.CCosto)
                    .WithMany(p => p.ProductosCCosto)
                    .HasForeignKey(d => d.IdCc)
                    .HasConstraintName("FK_PRODUCTOS_CCOSTO");

                entity.HasOne(d => d.ScCosto)
                    .WithMany(p => p.ProductosScCosto)
                    .HasForeignKey(d => d.IdScc)
                    .HasConstraintName("FK_PRODUCTOS_SCCOSTO");

                entity.HasOne(d => d.SscCosto)
                    .WithMany(p => p.ProductosSscCosto)
                    .HasForeignKey(d => d.IdSscc)
                    .HasConstraintName("FK_PRODUCTOS_SSCCOSTO");
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.HasKey(e => e.IdServ);

                entity.Property(e => e.IdServ).HasColumnName("ID_Serv");
                entity.Property(e => e.CodComercial).HasColumnName("Cod_Comercial");
                entity.Property(e => e.Descripcion).HasColumnName("Descripcion");
                entity.Property(e => e.NomCorto).HasColumnName("NomCorto");
                entity.Property(e => e.IdCc).HasColumnName("ID_CC").HasMaxLength(50);
                entity.Property(e => e.IdScc).HasColumnName("ID_SCC").HasMaxLength(50);
                entity.Property(e => e.IdSscc).HasColumnName("ID_SSCC").HasMaxLength(50);
                entity.Property(e => e.Estado).HasColumnName("Estado");

                entity.HasOne(d => d.CCosto)
                    .WithMany(p => p.ServiciosCCosto)
                    .HasForeignKey(d => d.IdCc)
                    .HasConstraintName("FK_SERVICIOS_CCOSTO");

                entity.HasOne(d => d.ScCosto)
                    .WithMany(p => p.ServiciosScCosto)
                    .HasForeignKey(d => d.IdScc)
                    .HasConstraintName("FK_SERVICIOS_SCCOSTO");

                entity.HasOne(d => d.SscCosto)
                    .WithMany(p => p.ServiciosSscCosto)
                    .HasForeignKey(d => d.IdSscc)
                    .HasConstraintName("FK_SERVICIOS_SSCCOSTO");
            });

            modelBuilder.Entity<Proyecto>(entity =>
            {
                entity.HasKey(e => e.IdProy);

                entity.Property(e => e.IdProy).HasColumnName("ID_Proy");
                entity.Property(e => e.CodComercial).HasColumnName("Cod_Comercial");
                entity.Property(e => e.Descripcion).HasColumnName("Descripcion");
                entity.Property(e => e.NomCorto).HasColumnName("NomCorto");
                entity.Property(e => e.IdCc).HasColumnName("ID_CC").HasMaxLength(50);
                entity.Property(e => e.IdScc).HasColumnName("ID_SCC").HasMaxLength(50);
                entity.Property(e => e.IdSscc).HasColumnName("ID_SSCC").HasMaxLength(50);
                entity.Property(e => e.Estado).HasColumnName("Estado");

                entity.HasOne(d => d.CCosto)
                    .WithMany(p => p.ProyectosCCosto)
                    .HasForeignKey(d => d.IdCc)
                    .HasConstraintName("FK_PROYECTOS_CCOSTO");

                entity.HasOne(d => d.ScCosto)
                    .WithMany(p => p.ProyectosScCosto)
                    .HasForeignKey(d => d.IdScc)
                    .HasConstraintName("FK_PROYECTOS_SCCOSTO");

                entity.HasOne(d => d.SscCosto)
                    .WithMany(p => p.ProyectosSscCosto)
                    .HasForeignKey(d => d.IdSscc)
                    .HasConstraintName("FK_PROYECTOS_SSCCOSTO");
            });

            modelBuilder.Entity<TipoDocumento>(entity =>
            {
                entity.HasKey(e => e.IdTdi);

                entity.Property(e => e.IdTdi).HasColumnName("ID_TDI");
                entity.Property(e => e.DescripcionTdi).HasColumnName("DescripcionTDI");
                entity.Property(e => e.NomCorto).HasColumnName("NomCorto");
                entity.Property(e => e.Estado).HasColumnName("Estado");
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.HasKey(e => e.IdMarca);

                entity.Property(e => e.IdMarca).HasColumnName("ID_Marca");
                entity.Property(e => e.NombreMarca).HasColumnName("NombreMarca");
                entity.Property(e => e.DescripcionMarca).HasColumnName("DescripcionMarca");
                entity.Property(e => e.NomCorto).HasColumnName("NomCorto");
                entity.Property(e => e.Estado).HasColumnName("Estado");
            });

            modelBuilder.Entity<TiposNegocio>(entity =>
            {
                entity.HasKey(e => e.IdTn);

                entity.Property(e => e.IdTn).HasColumnName("ID_TN");
                entity.Property(e => e.DescripcionTn).HasColumnName("DescripcionTN");
                entity.Property(e => e.NomCorto).HasColumnName("NomCorto");
                entity.Property(e => e.Estado).HasColumnName("Estado");
            });

            modelBuilder.Entity<StatusOp>(entity =>
            {
                entity.HasKey(e => e.IdStatus);

                entity.Property(e => e.IdStatus).HasColumnName("ID_Status");
                entity.Property(e => e.DescripcionStatus).HasColumnName("DescripcionStatus");
                entity.Property(e => e.NomCorto).HasColumnName("NomCorto");
                entity.Property(e => e.Estado).HasColumnName("Estado");
            });

            modelBuilder.Entity<CatFormaPago>(entity =>
            {
                entity.HasKey(e => e.IdCfp);

                entity.Property(e => e.IdCfp).HasColumnName("ID_CFP");
                entity.Property(e => e.DescripcionCfp).HasColumnName("DescripcionCFP");
                entity.Property(e => e.NomCorto).HasColumnName("NomCorto");
                entity.Property(e => e.Estado).HasColumnName("Estado");
            });

            modelBuilder.Entity<UnidadMedida>(entity =>
            {
                entity.HasKey(e => e.IdUm);

                entity.Property(e => e.IdUm).HasColumnName("ID_UM");
                entity.Property(e => e.NombreUm).HasColumnName("NombreUM");
                entity.Property(e => e.NomCorto).HasColumnName("NomCorto");
                entity.Property(e => e.Estado).HasColumnName("Estado");
            });

            modelBuilder.Entity<Clase>()
                .HasDiscriminator<string>("DiscriminatorClase")
                .HasValue<Clase>("Clase")
                .HasValue<SubClase>("SubClase")
                .HasValue<SubSubClase>("SubSubClase");

            modelBuilder.Entity<CCosto>()
                .HasDiscriminator<string>("DiscriminatorCosto")
                .HasValue<CCosto>("CCosto")
                .HasValue<ScCosto>("ScCosto")
                .HasValue<SscCosto>("SscCosto");

            modelBuilder.Entity<OrdenPedidoDetalle>(entity =>
            {
                entity.HasKey(e => e.IdOpd);

                entity.Property(e => e.IdOpd).HasColumnName("ID_OPD");
                entity.Property(e => e.IdStatus).HasColumnName("ID_Status");
                entity.Property(e => e.IdTn).HasColumnName("ID_TN");
                entity.Property(e => e.IdStn).HasColumnName("ID_STN");
                entity.Property(e => e.IdSstn).HasColumnName("ID_SSTN");
                entity.Property(e => e.IdProd).HasColumnName("ID_Prod");
                entity.Property(e => e.IdServ).HasColumnName("ID_Serv");
                entity.Property(e => e.IdProy).HasColumnName("ID_Proy");
                entity.Property(e => e.ItemOp).HasColumnName("Item_OP");
                entity.Property(e => e.CodComercial).HasColumnName("Cod_Comercial");
                entity.Property(e => e.Cantidad).HasColumnName("Cantidad");
                entity.Property(e => e.IdUm).HasColumnName("ID_UM");
                entity.Property(e => e.IdMda).HasColumnName("ID_Mda");
                entity.Property(e => e.Pvu).HasColumnName("PVU");
                entity.Property(e => e.FecReqCli).HasColumnName("FecReqCli");
                entity.Property(e => e.PtEstimado).HasColumnName("PT_Estimado");
                entity.Property(e => e.IdTc).HasColumnName("ID_TC");
                entity.Property(e => e.TeSem).HasColumnName("TE_Sem");
                entity.Property(e => e.NumCoti).HasColumnName("Num_Coti");
                entity.Property(e => e.IbArmado).HasColumnName("IB_Armado");
                entity.Property(e => e.CodCliente).HasColumnName("Cod_Cliente");
                entity.Property(e => e.NumDeal).HasColumnName("NumDeal");
                entity.Property(e => e.NumServicio).HasColumnName("Num_Servicio");
                entity.Property(e => e.NumProyecto).HasColumnName("Num_Proyecto");
                entity.Property(e => e.IdCc).HasColumnName("ID_CC").HasMaxLength(50);
                entity.Property(e => e.IdScc).HasColumnName("ID_SCC").HasMaxLength(50);
                entity.Property(e => e.IdSscc).HasColumnName("ID_SSCC").HasMaxLength(50);
                entity.Property(e => e.Nota1).HasColumnName("Nota1");
                entity.Property(e => e.Nota2).HasColumnName("Nota2");
                entity.Property(e => e.Nota3).HasColumnName("Nota3");
                entity.Property(e => e.Nota4).HasColumnName("Nota4");
                entity.Property(e => e.Estado).HasColumnName("Estado");

                entity.HasOne(d => d.StatusOp)
                    .WithMany(p => p.OrdenPedidoDetalle)
                    .HasForeignKey(d => d.IdStatus)
                    .HasConstraintName("FK_OPDETALLE_STATUS");

                entity.HasOne(d => d.TiposNegocio)
                    .WithMany(p => p.OrdenPedidoDetalle)
                    .HasForeignKey(d => d.IdTn)
                    .HasConstraintName("FK_OPDETALLE_TIPONEGOCIO");

                entity.HasOne(d => d.SubTiposNegocio)
                    .WithMany(p => p.OrdenPedidoDetalle)
                    .HasForeignKey(d => d.IdStn)
                    .HasConstraintName("FK_OPDETALLE_STNEGOCIO");

                entity.HasOne(d => d.SubSubTiposNegocio)
                    .WithMany(p => p.OrdenPedidoDetalle)
                    .HasForeignKey(d => d.IdSstn)
                    .HasConstraintName("FK_OPDETALLE_SSTNEGOCIO");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.OrdenPedidoDetalle)
                    .HasForeignKey(d => d.IdProd)
                    .HasConstraintName("FK_OPDETALLE_PRODUCTOS");

                entity.HasOne(d => d.Servicio)
                    .WithMany(p => p.OrdenPedidoDetalle)
                    .HasForeignKey(d => d.IdServ)
                    .HasConstraintName("FK_OPDETALLE_SERVICIOS");

                entity.HasOne(d => d.Proyecto)
                    .WithMany(p => p.OrdenPedidoDetalle)
                    .HasForeignKey(d => d.IdProy)
                    .HasConstraintName("FK_OPDETALLE_PROYECTOS");

                entity.HasOne(d => d.UnidadMedida)
                    .WithMany(p => p.OrdenPedidoDetalle)
                    .HasForeignKey(d => d.IdUm)
                    .HasConstraintName("FK_OPDETALLE_UNIDADMEDIDA");

                entity.HasOne(d => d.Moneda)
                    .WithMany(p => p.OrdenPedidoDetalle)
                    .HasForeignKey(d => d.IdMda)
                    .HasConstraintName("FK_OPDETALLE_MONEDAS");

                entity.HasOne(d => d.TcUsd)
                    .WithMany(p => p.OrdenPedidoDetalle)
                    .HasForeignKey(d => d.IdTc)
                    .HasConstraintName("FK_OPDETALLE_TCUSD");

                entity.HasOne(d => d.CCosto)
                    .WithMany(p => p.OrdenPedidoDetallesCCosto)
                    .HasForeignKey(d => d.IdCc)
                    .HasConstraintName("FK_OPDETALLE_CCOSTO");

                entity.HasOne(d => d.ScCosto)
                    .WithMany(p => p.OrdenPedidoDetallesScCosto)
                    .HasForeignKey(d => d.IdScc)
                    .HasConstraintName("FK_OPDETALLE_SCCOSTO");

                entity.HasOne(d => d.SscCosto)
                    .WithMany(p => p.OrdenPedidoDetallesSscCosto)
                    .HasForeignKey(d => d.IdSscc)
                    .HasConstraintName("FK_OPDETALLE_SSCCOSTO");

                entity.HasOne(d => d.OrdenPedido)
                    .WithMany(p => p.OrdenPedidoDetalles)
                    .HasForeignKey(d => d.IdOpci)
                    .HasConstraintName("FK_ORDENPEDIDODETALLE_ORDENPEDIDO");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasKey(e => e.IdLogin);
                entity.ToTable("LOGIN_CORSUSA");

                entity.Property(e => e.IdLogin).HasColumnName("ID_LOGIN");
                entity.Property(e => e.Usuario).HasColumnName("Usuario");
                entity.Property(e => e.Pass).HasColumnName("Pass");
                entity.Property(e => e.Estado).HasColumnName("Estado");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
