create database db_eccomerce;

use db_eccomerce;
go

create table tb_state(
	id int identity(1, 1) primary key,
	name varchar(15) unique not null
);
go

insert into tb_state (name) values ('activo'), ('inactivo'), ('eliminado');
go

