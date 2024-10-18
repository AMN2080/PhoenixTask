﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhoenixTask.Persistance;

#nullable disable

namespace PhoenixTask.Persistance.Migrations
{
    [DbContext(typeof(PhoenixDbContext))]
    partial class PhoenixDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PhoenixTask.Domain.Authorities.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Permission");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Name = "UpdateWorkSpace"
                        },
                        new
                        {
                            Id = 3,
                            Name = "DeleteWorkSpace"
                        },
                        new
                        {
                            Id = 4,
                            Name = "ReadWorkSpace"
                        },
                        new
                        {
                            Id = 5,
                            Name = "CreateProject"
                        },
                        new
                        {
                            Id = 6,
                            Name = "UpdateProject"
                        },
                        new
                        {
                            Id = 7,
                            Name = "DeleteProject"
                        },
                        new
                        {
                            Id = 8,
                            Name = "ReadProject"
                        },
                        new
                        {
                            Id = 9,
                            Name = "CreateBoard"
                        },
                        new
                        {
                            Id = 10,
                            Name = "UpdateBoard"
                        },
                        new
                        {
                            Id = 11,
                            Name = "DeleteBoard"
                        },
                        new
                        {
                            Id = 12,
                            Name = "ReadBoard"
                        },
                        new
                        {
                            Id = 13,
                            Name = "ManageUsers"
                        },
                        new
                        {
                            Id = 14,
                            Name = "ManageAdmin"
                        });
                });

            modelBuilder.Entity("PhoenixTask.Domain.Authorities.Role", b =>
                {
                    b.Property<int>("Value")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Value"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Value");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Value = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Value = 2,
                            Name = "ProjectManager"
                        },
                        new
                        {
                            Value = 3,
                            Name = "TeamMember"
                        },
                        new
                        {
                            Value = 4,
                            Name = "Viewer"
                        });
                });

            modelBuilder.Entity("PhoenixTask.Domain.Authorities.RolePermission", b =>
                {
                    b.Property<int>("RoleValue")
                        .HasColumnType("int");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.HasKey("RoleValue", "PermissionId");

                    b.ToTable("RolePermission");

                    b.HasData(
                        new
                        {
                            RoleValue = 1,
                            PermissionId = 2
                        },
                        new
                        {
                            RoleValue = 1,
                            PermissionId = 3
                        },
                        new
                        {
                            RoleValue = 1,
                            PermissionId = 4
                        },
                        new
                        {
                            RoleValue = 1,
                            PermissionId = 13
                        },
                        new
                        {
                            RoleValue = 1,
                            PermissionId = 5
                        },
                        new
                        {
                            RoleValue = 1,
                            PermissionId = 6
                        },
                        new
                        {
                            RoleValue = 1,
                            PermissionId = 7
                        },
                        new
                        {
                            RoleValue = 1,
                            PermissionId = 8
                        },
                        new
                        {
                            RoleValue = 1,
                            PermissionId = 9
                        },
                        new
                        {
                            RoleValue = 1,
                            PermissionId = 10
                        },
                        new
                        {
                            RoleValue = 1,
                            PermissionId = 11
                        },
                        new
                        {
                            RoleValue = 1,
                            PermissionId = 12
                        },
                        new
                        {
                            RoleValue = 1,
                            PermissionId = 14
                        },
                        new
                        {
                            RoleValue = 2,
                            PermissionId = 5
                        },
                        new
                        {
                            RoleValue = 2,
                            PermissionId = 6
                        },
                        new
                        {
                            RoleValue = 2,
                            PermissionId = 7
                        },
                        new
                        {
                            RoleValue = 2,
                            PermissionId = 8
                        },
                        new
                        {
                            RoleValue = 2,
                            PermissionId = 13
                        },
                        new
                        {
                            RoleValue = 2,
                            PermissionId = 9
                        },
                        new
                        {
                            RoleValue = 2,
                            PermissionId = 10
                        },
                        new
                        {
                            RoleValue = 2,
                            PermissionId = 11
                        },
                        new
                        {
                            RoleValue = 2,
                            PermissionId = 12
                        },
                        new
                        {
                            RoleValue = 3,
                            PermissionId = 4
                        },
                        new
                        {
                            RoleValue = 3,
                            PermissionId = 8
                        },
                        new
                        {
                            RoleValue = 3,
                            PermissionId = 12
                        },
                        new
                        {
                            RoleValue = 4,
                            PermissionId = 4
                        },
                        new
                        {
                            RoleValue = 4,
                            PermissionId = 8
                        },
                        new
                        {
                            RoleValue = 4,
                            PermissionId = 12
                        });
                });

            modelBuilder.Entity("PhoenixTask.Domain.Projects.Board", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Color");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("Order")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0)
                        .HasColumnName("Order");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Board");
                });

            modelBuilder.Entity("PhoenixTask.Domain.Projects.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("WorkSpaceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("WorkSpaceId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("PhoenixTask.Domain.Tasks.Task", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BoardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DeadLine")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.HasIndex("CreatorId");

                    b.ToTable("Task");
                });

            modelBuilder.Entity("PhoenixTask.Domain.Users.Member", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("MemberType")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoleValue")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleValue");

                    b.HasIndex("UserId");

                    b.ToTable("Member");

                    b.HasDiscriminator<int>("MemberType");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("PhoenixTask.Domain.Users.Setting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Setting");
                });

            modelBuilder.Entity("PhoenixTask.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsChangePassword")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("_authKey")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("AuthKey");

                    b.Property<string>("_passwordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PasswordHash");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("PhoenixTask.Domain.Workspaces.WorkSpace", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Color");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("WorkSpace");
                });

            modelBuilder.Entity("PhoenixTask.Domain.Projects.ProjectMember", b =>
                {
                    b.HasBaseType("PhoenixTask.Domain.Users.Member");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("PhoenixTask.Domain.Workspaces.WorkSpaceMember", b =>
                {
                    b.HasBaseType("PhoenixTask.Domain.Users.Member");

                    b.Property<Guid>("WorkSpaceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("PhoenixTask.Domain.Authorities.RolePermission", b =>
                {
                    b.HasOne("PhoenixTask.Domain.Authorities.Role", null)
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleValue")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PhoenixTask.Domain.Projects.Board", b =>
                {
                    b.HasOne("PhoenixTask.Domain.Projects.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("PhoenixTask.Domain.Workspaces.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("BoardId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("Name");

                            b1.HasKey("BoardId");

                            b1.ToTable("Board");

                            b1.WithOwner()
                                .HasForeignKey("BoardId");
                        });

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("PhoenixTask.Domain.Projects.Project", b =>
                {
                    b.HasOne("PhoenixTask.Domain.Workspaces.WorkSpace", null)
                        .WithMany()
                        .HasForeignKey("WorkSpaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("PhoenixTask.Domain.Workspaces.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("ProjectId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("Name");

                            b1.HasKey("ProjectId");

                            b1.ToTable("Project");

                            b1.WithOwner()
                                .HasForeignKey("ProjectId");
                        });

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("PhoenixTask.Domain.Tasks.Task", b =>
                {
                    b.HasOne("PhoenixTask.Domain.Projects.Board", null)
                        .WithMany()
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhoenixTask.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.OwnsOne("PhoenixTask.Domain.Workspaces.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("TaskId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("Name");

                            b1.HasKey("TaskId");

                            b1.ToTable("Task");

                            b1.WithOwner()
                                .HasForeignKey("TaskId");
                        });

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("PhoenixTask.Domain.Users.Member", b =>
                {
                    b.HasOne("PhoenixTask.Domain.Authorities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleValue")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhoenixTask.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PhoenixTask.Domain.Users.Setting", b =>
                {
                    b.HasOne("PhoenixTask.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("PhoenixTask.Domain.Users.Key", "Key", b1 =>
                        {
                            b1.Property<Guid>("SettingId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(130)
                                .HasColumnType("nvarchar(130)")
                                .HasColumnName("Key");

                            b1.HasKey("SettingId");

                            b1.ToTable("Setting");

                            b1.WithOwner()
                                .HasForeignKey("SettingId");
                        });

                    b.Navigation("Key")
                        .IsRequired();
                });

            modelBuilder.Entity("PhoenixTask.Domain.Users.User", b =>
                {
                    b.OwnsOne("PhoenixTask.Domain.Users.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)")
                                .HasColumnName("Email");

                            b1.HasKey("UserId");

                            b1.ToTable("User");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("PhoenixTask.Domain.Users.FirstName", "FirstName", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .HasMaxLength(35)
                                .HasColumnType("nvarchar(35)")
                                .HasColumnName("FirstName");

                            b1.HasKey("UserId");

                            b1.ToTable("User");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("PhoenixTask.Domain.Users.LastName", "LastName", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .HasMaxLength(35)
                                .HasColumnType("nvarchar(35)")
                                .HasColumnName("LastName");

                            b1.HasKey("UserId");

                            b1.ToTable("User");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("PhoenixTask.Domain.Users.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .HasMaxLength(11)
                                .HasColumnType("nvarchar(11)")
                                .HasColumnName("PhoneNumber");

                            b1.HasKey("UserId");

                            b1.ToTable("User");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("PhoenixTask.Domain.Users.UserName", "UserName", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(30)
                                .HasColumnType("nvarchar(30)")
                                .HasColumnName("UserName");

                            b1.HasKey("UserId");

                            b1.ToTable("User");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("FirstName");

                    b.Navigation("LastName");

                    b.Navigation("PhoneNumber");

                    b.Navigation("UserName")
                        .IsRequired();
                });

            modelBuilder.Entity("PhoenixTask.Domain.Workspaces.WorkSpace", b =>
                {
                    b.HasOne("PhoenixTask.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("PhoenixTask.Domain.Workspaces.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("WorkSpaceId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("Name");

                            b1.HasKey("WorkSpaceId");

                            b1.ToTable("WorkSpace");

                            b1.WithOwner()
                                .HasForeignKey("WorkSpaceId");
                        });

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("PhoenixTask.Domain.Authorities.Role", b =>
                {
                    b.Navigation("RolePermissions");
                });
#pragma warning restore 612, 618
        }
    }
}
