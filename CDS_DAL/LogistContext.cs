using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using CDS_Models;
using CDS_Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
        public  DbSet<TipoDocsIdent> TiposDocumento { get; set; }
        public  DbSet<Marca> Marcas { get; set; }
        public  DbSet<SubTipoNegocio> SubTiposNegocio { get; set; }
        public  DbSet<TipoNegocio> TipoNegocio { get; set; }
        public  DbSet<SubSubTipoNegocio> SubSubTiposNegocio { get; set; }
        public  DbSet<EstadosOp> StatusOp { get; set; }
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
                var conn = Environment.GetEnvironmentVariable("CONNECTION_STRING");
                if (!string.IsNullOrEmpty(conn))
                {
                    System.Console.WriteLine("[DEBUG] Usando CONNECTION_STRING");
                    optionsBuilder.UseSqlServer(conn, sqlOptions =>
                    {
                        sqlOptions.CommandTimeout(3600);
                        sqlOptions.EnableRetryOnFailure();
                    });
                }
                else
                {
                    System.Console.WriteLine("[ERROR] No se encontró la variable de entorno CONNECTION_STRING para la cadena de conexión");
                    throw new InvalidOperationException("No se encontró la variable de entorno CONNECTION_STRING para la cadena de conexión");
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProdUm>(entity =>
            {
                entity.ToTable("Prod_UM");
                entity.HasKey(e => new { e.IdPrd, e.IdUm });

                entity.Property(e => e.IdPrd).HasColumnName("Id_Prd");
                entity.Property(e => e.IdUm).HasColumnName("Id_UM");
                entity.Property(e => e.Descrip).HasColumnName("Descrip");
                entity.Property(e => e.Estado).HasColumnName("Estado");

                entity.HasOne(d => d.Productos)
                    .WithMany(p => p.ProdUm)
                    .HasForeignKey(d => d.IdPrd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRODUM_PRODUCTOS");

                entity.HasOne(d => d.UnidadesMedida)
                    .WithMany(p => p.ProdUm)
                    .HasForeignKey(d => d.IdUm)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRODUM_UNIDADMEDIDA");
            });

            modelBuilder.Entity<OrdenPedido>(entity =>
            {
                entity.ToTable("OrdPedido");
                entity.HasKey(e => e.IdOpci);

                entity.Property(e => e.IdOpci).HasColumnName("Id_OPCI");
                entity.Property(e => e.IdFp).HasColumnName("Id_FP");
                entity.Property(e => e.IdClt).HasColumnName("Id_Clt");
                entity.Property(e => e.IdVdr).HasColumnName("Id_Vdr");
                entity.Property(e => e.FecRecep).HasColumnName("FecRecep");
                entity.Property(e => e.FecInicio).HasColumnName("FecInicio");
                entity.Property(e => e.FecProcVi).HasColumnName("FecProcVI");
                entity.Property(e => e.RsocialClt).HasColumnName("RSocialClt");
                entity.Property(e => e.NumOp).HasColumnName("NumOP");
                entity.Property(e => e.IdMda).HasColumnName("Id_Mda");
                entity.Property(e => e.TotalSinIgv).HasColumnName("TotalSinIGV").HasColumnType("decimal(18,2)");
                entity.Property(e => e.NumRefCliente).HasColumnName("NumRefCliente");
                entity.Property(e => e.IbCltFin).HasColumnName("Ib_CltFinal");
                entity.Property(e => e.IbCltPrv).HasColumnName("Ib_CltPrv");
                entity.Property(e => e.IbVdr1).HasColumnName("Ib_Vdr1");
                entity.Property(e => e.IbVdr2).HasColumnName("Ib_Vdr2");
                entity.Property(e => e.IbLider).HasColumnName("Ib_Lider");
                entity.Property(e => e.UbrutaCoti).HasColumnName("UbrutaCoti");
                entity.Property(e => e.ComisionCompartida).HasColumnName("ComisionCompartida");
                entity.Property(e => e.Estado).HasColumnName("Estado");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.OrdenPedido)
                    .HasForeignKey(d => d.IdClt)
                    .HasConstraintName("FK_ORDENPEDIDO_CLIENTE");

                entity.HasOne(d => d.Vendedor)
                    .WithMany(p => p.OrdenPedido)
                    .HasForeignKey(d => d.IdVdr)
                    .HasConstraintName("FK_ORDENPEDIDO_VENDEDORES");

                entity.HasOne(d => d.Moneda)
                    .WithMany()
                    .HasForeignKey(d => d.IdMda)
                    .HasConstraintName("FK_ORDENPEDIDO_MONEDAS");

                entity.HasOne(d => d.FormaPago)
                    .WithMany(p => p.OrdenesPedido)
                    .HasForeignKey(d => d.IdFp)
                    .HasConstraintName("FK_ORDENPEDIDO_FORMAPAGO");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");
                entity.HasKey(e => e.IdClt);

                entity.Property(e => e.IdClt).HasColumnName("Id_Clt");
                entity.Property(e => e.IdTdi).HasColumnName("Id_TDI");
                entity.Property(e => e.NDoc).HasColumnName("NDoc");
                entity.Property(e => e.RazonSocial).HasColumnName("RazonSocial");
                entity.Property(e => e.CorreoClt).HasColumnName("CorreoClt");
                entity.Property(e => e.TelefClt).HasColumnName("TelefClt");
                entity.Property(e => e.DirecClt).HasColumnName("DirecClt");
                entity.Property(e => e.IbCltPrv).HasColumnName("Ib_CltPrv");
                entity.Property(e => e.IbCltFinal).HasColumnName("Ib_CltFinal");
                entity.Property(e => e.Estado).HasColumnName("Estado");

                entity.HasOne(d => d.TipoDocumento)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.IdTdi)
                    .HasConstraintName("FK_CLIENTE_TIPODOCUMENTO");
            });

            modelBuilder.Entity<Vendedor>(entity =>
            {
                entity.ToTable("Vendedor");
                entity.HasKey(e => e.IdVdr);

                entity.Property(e => e.IdVdr).HasColumnName("Id_Vdr");
                entity.Property(e => e.IdTdi).HasColumnName("Id_TDI");
                entity.Property(e => e.NDoc).HasColumnName("NDoc");
                entity.Property(e => e.NomVdr).HasColumnName("NomVdr");
                entity.Property(e => e.IbLider).HasColumnName("IB_Lider");
                entity.Property(e => e.Estado).HasColumnName("Estado");

                entity.HasOne(d => d.TipoDocumento)
                    .WithMany(p => p.Vendedores)
                    .HasForeignKey(d => d.IdTdi)
                    .HasConstraintName("FK_VENDEDORES_TIPODOCUMENTO");
            });

            modelBuilder.Entity<SubTipoNegocio>(static entity =>
            {
                entity.ToTable("TipNeg_Sub");
                entity.HasKey(e => new { e.IdTn, e.IdStn });

                entity.Property(e => e.IdTn).HasColumnName("Id_TN");
                entity.Property(e => e.IdStn).HasColumnName("Id_STN");
                entity.Property(e => e.Descrip).HasColumnName("Descrip");
                entity.Property(e => e.NCorto).HasColumnName("NCorto");
                entity.Property(e => e.Estado).HasColumnName("Estado");

                entity.HasOne(d => d.TipoNegocio)
                    .WithMany(p => p.SubTiposNegocio)
                    .HasForeignKey(d => d.IdTn)
                    .HasConstraintName("FK_STNEGOCIO_TIPONEGOCIO");
            });

            modelBuilder.Entity<SubSubTipoNegocio>(entity =>
            {
                entity.ToTable("TipNeg_SubSub");
                entity.HasKey(e => new { e.IdTn, e.IdStn, e.IdSstn });

                entity.Property(e => e.IdTn).HasColumnName("Id_TN");
                entity.Property(e => e.IdStn).HasColumnName("Id_STN");
                entity.Property(e => e.IdSstn).HasColumnName("Id_SSTN");
                entity.Property(e => e.Descrip).HasColumnName("Descrip");
                entity.Property(e => e.NCorto).HasColumnName("NCorto");
                entity.Property(e => e.Estado).HasColumnName("Estado");

                entity.HasOne(d => d.SubTipoNegocio)
                    .WithMany(p => p.SubSubTipoNegocio)
                    .HasForeignKey(d => new { d.IdTn, d.IdStn })
                    .HasConstraintName("FK_SSTN_STNEGOCIO");
            });

            modelBuilder.Entity<FormaPago>(entity =>
            {
                entity.ToTable("FormaPago");
                entity.HasKey(e => e.IdFp);

                entity.Property(e => e.IdFp).HasColumnName("Id_FP");
                entity.Property(e => e.IdCfp).HasColumnName("Id_CFP");
                entity.Property(e => e.Descrip).HasColumnName("Descrip");
                entity.Property(e => e.NCorto).HasColumnName("NCorto");
                entity.Property(e => e.Estado).HasColumnName("Estado");
            });

            modelBuilder.Entity<CatFormaPago>(static entity =>
            {
                entity.ToTable("Cat_Forma_Pago");
                entity.HasKey(e => e.IdCfp);

                entity.Property(e => e.IdCfp).HasColumnName("Id_CFP");
                entity.Property(e => e.Descrip).HasColumnName("Descrip");
                entity.Property(e => e.NCorto).HasColumnName("NCorto");
                entity.Property(e => e.Estado).HasColumnName("Estado");
                entity.HasMany(cfp => cfp.FormaPago)
                    .WithOne(fp => fp.CatFormaPago)
                    .HasForeignKey(fp => fp.IdCfp)
                    .HasConstraintName("FK_FORMAPAGO_CATFORMAPAGO");
            });

            modelBuilder.Entity<TcUsd>(entity =>
            {
                entity.ToTable("TipoCambio");
                entity.HasKey(e => e.IdTc);

                entity.Property(e => e.IdTc).HasColumnName("Id_TC");
                entity.Property(e => e.IdMda).HasColumnName("Id_Mda");
                entity.Property(e => e.FechaTc).HasColumnName("FechaTC");
                entity.Property(e => e.TipCam).HasColumnName("TipCam");

                entity.HasOne(d => d.Moneda)
                    .WithMany()
                    .HasForeignKey(d => d.IdMda)
                    .HasConstraintName("FK_TCUSD_MONEDAS");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Producto");
                entity.HasKey(e => e.IdPrd);

                entity.Property(e => e.IdPrd).HasColumnName("Id_Prd");
                entity.Property(e => e.IdMca).HasColumnName("Id_Mca");
                entity.Property(e => e.CodCom1).HasColumnName("CodCom1");
                entity.Property(e => e.CodCom2).HasColumnName("CodCom2");
                entity.Property(e => e.CodCom3).HasColumnName("CodCom3");
                entity.Property(e => e.Descrip).HasColumnName("Descrip");
                entity.Property(e => e.NCorto).HasColumnName("NCorto");
                entity.Property(e => e.IdCls).HasColumnName("Id_Cls");
                entity.Property(e => e.IdSCls).HasColumnName("Id_SCls");
                entity.Property(e => e.IdSsCls).HasColumnName("Id_SSCls");
                entity.Property(e => e.IdCc).HasColumnName("Id_CC").HasMaxLength(50);
                entity.Property(e => e.IdScC).HasColumnName("Id_SCC").HasMaxLength(50);
                entity.Property(e => e.IdSscC).HasColumnName("Id_SSCC").HasMaxLength(50);
                entity.Property(e => e.Estado).HasColumnName("Estado");

                entity.HasOne(d => d.Marca)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdMca)
                    .HasConstraintName("FK_PRODUCTOS_MARCA");

                entity.HasOne(d => d.Clase)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCls)
                    .HasConstraintName("FK_PRODUCTOS_CLASE");

                entity.HasOne(d => d.SubClase)
                    .WithMany(p => p.ProductosSubClase)
                    .HasForeignKey(d => d.IdSCls)
                    .HasConstraintName("FK_PRODUCTOS_SUBCLASE");

                entity.HasOne(d => d.SubSubClase)
                    .WithMany(p => p.ProductosSubSubClase)
                    .HasForeignKey(d => d.IdSsCls)
                    .HasConstraintName("FK_PRODUCTOS_SSCLASE");

                entity.HasOne(d => d.CCosto)
                    .WithMany(p => p.ProductosCCosto)
                    .HasForeignKey(d => d.IdCc)
                    .HasConstraintName("FK_PRODUCTOS_CCOSTO");

                entity.HasOne(d => d.ScCosto)
                    .WithMany(p => p.ProductosScCosto)
                    .HasForeignKey(d => new { d.IdCc, d.IdScC })
                    .HasConstraintName("FK_PRODUCTOS_SCCOSTO");

                entity.HasOne(d => d.SscCosto)
                    .WithMany(p => p.ProductosSscCosto)
                    .HasForeignKey(d => new { d.IdCc, d.IdScC, d.IdSscC })
                    .HasConstraintName("FK_PRODUCTOS_SSCCOSTO");
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.ToTable("Servicio");
                entity.HasKey(e => e.IdSrv);

                entity.Property(e => e.IdSrv).HasColumnName("Id_Srv");
                entity.Property(e => e.CodCom1).HasColumnName("CodCom1");
                entity.Property(e => e.Descrip).HasColumnName("Descrip");
                entity.Property(e => e.NCorto).HasColumnName("NCorto");
                entity.Property(e => e.IdCc).HasColumnName("Id_CC").HasMaxLength(50);
                entity.Property(e => e.IdScC).HasColumnName("Id_SCC").HasMaxLength(50);
                entity.Property(e => e.IdSscC).HasColumnName("Id_SSCC").HasMaxLength(50);
                entity.Property(e => e.Estado).HasColumnName("Estado");

                entity.HasOne(d => d.CCosto)
                    .WithMany(p => p.ServiciosCCosto)
                    .HasForeignKey(d => d.IdCc)
                    .HasConstraintName("FK_SERVICIOS_CCOSTO");

                entity.HasOne(d => d.ScCosto)
                    .WithMany(p => p.ServiciosScCosto)
                    .HasForeignKey(d => new { d.IdCc, d.IdScC })
                    .HasConstraintName("FK_SERVICIOS_SCCOSTO");

                entity.HasOne(d => d.SscCosto)
                    .WithMany(p => p.ServiciosSscCosto)
                    .HasForeignKey(d => new { d.IdCc, d.IdScC, d.IdSscC })
                    .HasConstraintName("FK_SERVICIOS_SSCCOSTO");
            });

            modelBuilder.Entity<Proyecto>(entity =>
            {
                entity.ToTable("Proyecto");
                entity.HasKey(e => e.IdPry);

                entity.Property(e => e.IdPry).HasColumnName("Id_Pry");
                entity.Property(e => e.CodCom1).HasColumnName("CodComercial");
                entity.Property(e => e.Descrip).HasColumnName("Descrip");
                entity.Property(e => e.NCorto).HasColumnName("NCorto");
                entity.Property(e => e.IdCc).HasColumnName("Id_CC").HasMaxLength(50);
                entity.Property(e => e.IdScC).HasColumnName("Id_SCC").HasMaxLength(50);
                entity.Property(e => e.IdSscC).HasColumnName("Id_SSCC").HasMaxLength(50);
                entity.Property(e => e.Estado).HasColumnName("Estado");

                entity.HasOne(d => d.CCosto)
                    .WithMany(p => p.ProyectosCCosto)
                    .HasForeignKey(d => d.IdCc)
                    .HasConstraintName("FK_PROYECTOS_CCOSTO");

                entity.HasOne(d => d.ScCosto)
                    .WithMany(p => p.ProyectosScCosto)
                    .HasForeignKey(d => new { d.IdCc, d.IdScC })
                    .HasConstraintName("FK_PROYECTOS_SCCOSTO");

                entity.HasOne(d => d.SscCosto)
                    .WithMany(p => p.ProyectosSscCosto)
                    .HasForeignKey(d => new { d.IdCc, d.IdScC, d.IdSscC })
                    .HasConstraintName("FK_PROYECTOS_SSCCOSTO");
            });

            modelBuilder.Entity<TipoDocsIdent>(entity =>
            {
                entity.ToTable("TipoDocsIdent");
                entity.HasKey(e => e.IdTdi);

                entity.Property(e => e.IdTdi).HasColumnName("Id_TDI");
                entity.Property(e => e.Descrip).HasColumnName("Descrip");
                entity.Property(e => e.NCorto).HasColumnName("NomCorto");
                entity.Property(e => e.Estado).HasColumnName("Estado");
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.ToTable("Marca");
                entity.HasKey(e => e.IdMca);

                entity.Property(e => e.IdMca).HasColumnName("Id_Mca");
                entity.Property(e => e.NomMarca).HasColumnName("NomMarca");
                entity.Property(e => e.Descrip).HasColumnName("Descrip");
                entity.Property(e => e.NCorto).HasColumnName("NCorto");
            });

            modelBuilder.Entity<TipoNegocio>(entity =>
            {
                entity.ToTable("TipoNegocio");
                entity.HasKey(e => e.IdTn);

                entity.Property(e => e.IdTn).HasColumnName("Id_TN");
                entity.Property(e => e.Descrip).HasColumnName("Descrip");
                entity.Property(e => e.NCorto).HasColumnName("NCorto");
                entity.Property(e => e.Estado).HasColumnName("Estado");
            });

            modelBuilder.Entity<EstadosOp>(entity =>
            {
                entity.ToTable("EstadosOP");
                entity.HasKey(e => e.IdEstOp);

                entity.Property(e => e.IdEstOp).HasColumnName("Id_EstOP");
                entity.Property(e => e.Descrip).HasColumnName("Descrip");
                entity.Property(e => e.NCorto).HasColumnName("NCorto");
                //entity.Property(e => e.Estado).HasColumnName("Estado");
            });

            modelBuilder.Entity<UnidadMedida>(entity =>
            {
                entity.ToTable("UnidadMedida");
                entity.HasKey(e => e.IdUm);

                entity.Property(e => e.IdUm).HasColumnName("Id_UM");
                entity.Property(e => e.Nombre).HasColumnName("Nombre");
                entity.Property(e => e.NCorto).HasColumnName("NCorto");
                entity.Property(e => e.Estado).HasColumnName("Estado");
            });

            modelBuilder.Entity<Moneda>(entity =>
            {
                entity.ToTable("Moneda");
                entity.HasKey(e => e.IdMda);
                entity.Property(e => e.IdMda).HasColumnName("Id_Mda");
                entity.Property(e => e.Nombre).HasColumnName("Nombre");
                entity.Property(e => e.EquivSunat).HasColumnName("Equiv_Sunat");
                entity.Property(e => e.Estado).HasColumnName("Estado");
            });

            modelBuilder.Entity<Clase>()
                .HasDiscriminator<string>("DiscriminatorClase")
                .HasValue<Clase>("Clase")
                .HasValue<SubClase>("SubClase")
                .HasValue<SubSubClase>("SubSubClase");

            modelBuilder.Entity<ScCosto>(entity =>
            {
                entity.ToTable("SCCosto");
                entity.HasKey(e => new { e.IdCc, e.IdScC });

                entity.HasOne(d => d.CCosto)
                    .WithMany()
                    .HasForeignKey(d => d.IdCc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SCCOSTO_CCOSTO");
            });

            modelBuilder.Entity<SscCosto>(entity =>
            {
                entity.ToTable("SSCCosto");
                entity.HasKey(e => new { e.IdCc, e.IdScC, e.IdSscC });

                entity.HasOne(d => d.ScCosto)
                    .WithMany(p => p.SscCosto)
                    .HasForeignKey(d => new { d.IdCc, d.IdScC })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SSCCOSTO_SCCOSTO");
            });

            modelBuilder.Entity<OrdenPedidoDetalle>(entity =>
            {
                entity.ToTable("OrdPedidoDet");
                entity.HasKey(e => e.IdOpci);

                //entity.Property(e => e.IdOpd).HasColumnName("Id");
                entity.Property(e => e.IdOpci).HasColumnName("Id_OPCI");
                entity.Property(e => e.IdPrd).HasColumnName("Id_Prd");
                entity.Property(e => e.IdSrv).HasColumnName("Id_Srv");
                entity.Property(e => e.IdPry).HasColumnName("Id_Pry");
                entity.Property(e => e.ItemOp).HasColumnName("ItemOp");
                entity.Property(e => e.CodCom1).HasColumnName("CodCom1");
                entity.Property(e => e.Cant).HasColumnName("Cant");
                entity.Property(e => e.IdUm).HasColumnName("Id_UM");
                entity.Property(e => e.IdMda).HasColumnName("Id_Mda");
                entity.Property(e => e.PreVentUn).HasColumnName("PrecioVentaUnit");
                entity.Property(e => e.FecReqCli).HasColumnName("FecReqCli");
                entity.Property(e => e.PtEstimado).HasColumnName("PT_Estimado");
                entity.Property(e => e.IdTc).HasColumnName("Id_TC");
                entity.Property(e => e.IdTn).HasColumnName("Id_TN");
                entity.Property(e => e.IdStn).HasColumnName("Id_STN");
                entity.Property(e => e.IdSstn).HasColumnName("Id_SSTN");
                entity.Property(e => e.TeSem).HasColumnName("TE_Sem");
                entity.Property(e => e.NumCoti).HasColumnName("NumCoti");
                entity.Property(e => e.IbArmado).HasColumnName("Ib_Armado");
                entity.Property(e => e.CodCliente).HasColumnName("Cod_Cliente");
                entity.Property(e => e.NumDeal).HasColumnName("NumDeal");
                entity.Property(e => e.NumSrv).HasColumnName("NumSrv");
                entity.Property(e => e.NumPry).HasColumnName("NumPry");
                entity.Property(e => e.IdCc).HasColumnName("Id_CC");
                entity.Property(e => e.IdScC).HasColumnName("Id_SCC");
                entity.Property(e => e.IdSscC).HasColumnName("Id_SSCC");
                entity.Property(e => e.Nota1).HasColumnName("Nota1");
                entity.Property(e => e.Nota2).HasColumnName("Nota2");
                entity.Property(e => e.Nota3).HasColumnName("Nota3");
                entity.Property(e => e.Nota4).HasColumnName("Nota4");

                //entity.HasOne(d => d.EstadosOp)
                //    .WithMany(p => p.OrdenPedidoDetalle)
                //    .HasForeignKey(d => d.IdEstOp)
                //    .HasConstraintName("FK_OPDETALLE_STATUS");

                entity.HasOne(d => d.TipoNegocio)
                    .WithMany(p => p.OrdenPedidoDetalle)
                    .HasForeignKey(d => d.IdTn)
                    .HasConstraintName("FK_OPDETALLE_TIPONEGOCIO");

                entity.HasOne(d => d.SubTiposNegocio)
                    .WithMany(p => p.OrdenPedidoDetalle)
                    .HasForeignKey(d => new { d.IdTn, d.IdStn })
                    .HasConstraintName("FK_OPDETALLE_STNEGOCIO");

                entity.HasOne(d => d.SubSubTiposNegocio)
                    .WithMany(p => p.OrdenPedidoDetalle)
                    .HasForeignKey(d => new { d.IdTn, d.IdStn, d.IdSstn })
                    .HasConstraintName("FK_OPDETALLE_SSTNEGOCIO");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.OrdenPedidoDetalle)
                    .HasForeignKey(d => d.IdPrd)
                    .HasConstraintName("FK_OPDETALLE_PRODUCTOS");

                entity.HasOne(d => d.Servicio)
                    .WithMany(p => p.OrdenPedidoDetalle)
                    .HasForeignKey(d => d.IdSrv)
                    .HasConstraintName("FK_OPDETALLE_SERVICIOS");

                entity.HasOne(d => d.Proyecto)
                    .WithMany(p => p.OrdenPedidoDetalle)
                    .HasForeignKey(d => d.IdPry)
                    .HasConstraintName("FK_OPDETALLE_PROYECTOS");

                entity.HasOne(d => d.UnidadMedida)
                    .WithMany(p => p.OrdenPedidoDetalle)
                    .HasForeignKey(d => d.IdUm)
                    .HasConstraintName("FK_OPDETALLE_UNIDADMEDIDA");

                entity.HasOne(d => d.Moneda)
                    .WithMany()
                    .HasForeignKey(d => d.IdMda)
                    .HasConstraintName("FK_OPDETALLE_MONEDAS");

                entity.HasOne(d => d.TcUsd)
                    .WithMany(p => p.OrdenPedidoDetalle)
                    .HasForeignKey(d => d.IdTc)
                    .HasConstraintName("FK_OPDETALLE_TCUSD");


                entity.HasOne(d => d.CCosto)
                    .WithMany(p => p.OrdenPedidoDetallesCCosto)
                    .HasForeignKey(d => d.IdCc)
                    .HasPrincipalKey(p => p.IdCc)
                    .HasConstraintName("FK_OPDETALLE_CCOSTO");

                entity.HasOne(d => d.ScCosto)
                    .WithMany(p => p.OrdenPedidoDetallesScCosto)
                    .HasForeignKey(d => new { d.IdCc, d.IdScC })
                    .HasPrincipalKey(p => new { p.IdCc, p.IdScC })
                    .HasConstraintName("FK_OPDETALLE_SCCOSTO");

                entity.HasOne(d => d.SscCosto)
                    .WithMany(p => p.OrdenPedidoDetallesSscCosto)
                    .HasForeignKey(d => new { d.IdCc, d.IdScC , d.IdSscC } )
                    .HasPrincipalKey(p => new { p.IdCc, p.IdScC, p.IdSscC })
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
