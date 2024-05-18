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
