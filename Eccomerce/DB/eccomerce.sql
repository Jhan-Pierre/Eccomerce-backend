-- Eliminar la base de datos si ya existe
if exists (select * from sys.databases where name = 'db_eccomerce')
begin
    drop database db_eccomerce;
end
go

-- Crear la base de datos
create database db_eccomerce;
go

-- Usar la base de datos
use db_eccomerce;
go

-- Eliminar las tablas si ya existen
if exists (select * from sys.objects where object_id = object_id(N'[dbo].[tb_state]') and type in (N'U'))
begin
    drop table [dbo].[tb_state];
end
go

if exists (select * from sys.objects where object_id = object_id(N'[dbo].[tb_shift]') and type in (N'U'))
begin
    drop table [dbo].[tb_shift];
end
go

if exists (select * from sys.objects where object_id = object_id(N'[dbo].[tb_role]') and type in (N'U'))
begin
    drop table [dbo].[tb_role];
end
go

if exists (select * from sys.objects where object_id = object_id(N'[dbo].[tb_module]') and type in (N'U'))
begin
    drop table [dbo].[tb_module];
end
go

if exists (select * from sys.objects where object_id = object_id(N'[dbo].[tb_permission]') and type in (N'U'))
begin
    drop table [dbo].[tb_permission];
end
go

if exists (select * from sys.objects where object_id = object_id(N'[dbo].[tb_role_permission]') and type in (N'U'))
begin
    drop table [dbo].[tb_role_permission];
end
go

if exists (select * from sys.objects where object_id = object_id(N'[dbo].[tb_user]') and type in (N'U'))
begin
    drop table [dbo].[tb_user];
end
go

-- Crear las tablas
create table tb_state(
    id int identity(1, 1) primary key,
    name varchar(15) unique not null
);
go

create table tb_shift (
    id int identity(1, 1) primary key,
    name varchar(80) not null unique
);
go

create table tb_role (
    id int identity(1, 1) primary key,
    name varchar(40) not null unique
);
go

create table tb_module (
    id bigint identity(1, 1) primary key,
    name varchar(80) not null unique
);
go

create table tb_permission (
    id bigint identity(1, 1) primary key,
    name varchar(80) not null unique,
    module_id bigint,
    foreign key (module_id) references tb_module(id)
);
go

create table tb_role_permission (
    id bigint identity(1, 1) primary key,
    role_id int,
    permission_id bigint,
    foreign key (role_id) references tb_role(id),
    foreign key (permission_id) references tb_permission(id)
);
go

create table tb_user (
    id bigint identity(1, 1) primary key,
    email varchar(60) not null unique,
    password varchar(200) not null,
    phone char(9) not null unique,
    first_name varchar(80) not null,
    last_name varchar(80) not null,
    created_at datetime,
    updated_at datetime,
    role_id int not null,
    state_id int not null,
    shift_id int,
    foreign key (role_id) references tb_role(id),
    foreign key (state_id) references tb_state(id),
    foreign key (shift_id) references tb_shift(id)
);
go

-- Insertar datos en tb_state
insert into tb_state (name) values ('activo'), ('inactivo'), ('eliminado');
go

-- Insertar datos en tb_shift
insert into tb_shift (name) values ('mañana'), ('tarde'), ('noche');
go

-- Insertar datos en tb_role
insert into tb_role (name) values ('admin'), ('empleado');
go

-- Insertar datos en tb_module
insert into tb_module values ('role'),('usuario'),('producto'),('categoria_producto'),('proveedor'),('pedido'),('venta');
go

-- Insertar datos en tb_permission
insert into tb_permission (name, module_id) values ('agregar_rol', 1),('editar_rol', 1),('eliminar_rol', 1),('ver_rol', 1),('buscar_rol', 1),
('agregar_usuario', 2),('editar_usuario', 2),('eliminar_usuario', 2),('ver_usuario', 2),('buscar_usuario', 2),
('agregar_producto', 3),('editar_producto', 3),('eliminar_producto', 3),('ver_producto', 3),('buscar_producto', 3),
('agregar_categoria_producto', 4),('editar_categoria_producto', 4),('eliminar_categoria_producto', 4),('ver_categoria_producto', 4),('buscar_categoria_producto', 4),
('agregar_proveedor', 5),('editar_proveedor', 5),('eliminar_proveedor', 5),('ver_proveedor', 5),('buscar_proveedor', 5),
('agregar_pedido', 6),('editar_pedido', 6),('eliminar_pedido', 6),('ver_pedido', 6),('buscar_pedido', 6),
('agregar_venta', 7),('editar_venta', 7),('eliminar_venta', 7),('ver_venta', 7),('buscar_venta', 7);
go

-- Insertar datos en tb_role_permission
insert into tb_role_permission (role_id, permission_id) values (1, 1), (1, 2), (1, 3), (1, 4), (1, 5), (1, 6), (1, 7), (1, 8), (1, 9), (1, 10), (1, 11), (1, 12), (1, 13), (1, 14), (1, 15), (1, 16), (1, 17), (1, 18), (1, 19), (1, 20), (2, 14), (2, 15), (2, 19), (2, 31), (2, 34), (2, 35), (1, 21), (1, 22), (1, 23), (1, 24), (1, 25), (1, 26), (1, 27), (1, 28), (1, 29), (1, 30), (1, 31), (1, 32), (1, 33), (1, 34), (1, 35);
go

-- Insertar datos en tb_user
insert into tb_user (email, password, phone, first_name, last_name, created_at, updated_at, role_id, state_id, shift_id)
values 
    ('juan@example.com', '$2y$12$cj3/SNIEMQUqEeZjO.btxuQav0U.ySoHv35rOKb1IGcLVx3p1tkIO', '963754812', 'Juan', 'Perez', '2024-05-06 02:19:51', '2024-05-06 02:19:51', 2, 1, NULL),
    ('maria@example.com', '$2y$12$V59gK64Xx.awhZU9v0MnUOCS8h8At6VY69k2vzV5UMjA4EbWm8tUO', '964578132', 'María', 'García', '2024-05-06 02:19:51', '2024-05-06 02:19:51', 2, 1, NULL),
    ('admin@minimarketlaravel.io', '$2y$12$V59gK64Xx.awhZU9v0MnUOCS8h8At6VY69k2vzV5UMjA4EbWm8tUO', '946758213', 'Admin', '', '2024-05-06 02:19:53', '2024-05-06 02:19:53', 1, 1, NULL);
go
