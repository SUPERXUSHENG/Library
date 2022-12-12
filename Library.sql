create database Library 
go
use Library

--管理员表
create table Manager
(
	managerid char(20) primary key,--账号
	managerPassword char(12) not null,--密码
	mIdentity bit not null default 1,--身份(管理员-1、工作人员-0)
)

--图书类别表
create table BookCategory
(
	bCategoryid int primary key,--图书类别编号
	bCategoryname nchar(16) not null,--图书类别名称
)

--图书信息表
create table BookList
(
	ISBN char(25) primary key,--ISBN
	BookName nchar(30) not null, --书名
	bCategoryid int not null foreign key references BookCategory,--图书类别编号
	author nchar(20) not null, --第一作者
	publisher nchar(30) not null,--第一出版社
	publishTime int not null, --出版年份
	bookstate bit not null default 1, --状态（正常-1，报废-0）
	num int not null, --库存数目
	lendnum int not null, --借出数目 
	price float not null --单价
)

--读者类别
create table ReaderCategory
(
	Rcategoryid int not null primary key, --读者类别编号 
	Rcategoryname nchar(10) not null, --读者类别名称 
	Rbnum int not null, --可借书数目
	Rday int not null --可借书天数
)

--读者信息表
create table Reader
(
	Rid char(19) primary key, --身份证号
	Rname nchar(10) not null, --姓名
	Rcategoryid int not null foreign key references ReaderCategory, --类别（学生，教师等）
	Phone char(11), --电话
	RbLnum int not null, --已借书数目
)

--借书记录明细
create table BookLendList
(
	Rid char(19) not null foreign key references Reader, --读者身份证号
	ISBN char(25) not null foreign key references BookList,--ISBN
	LendTime datetime not null Default getdate(), --借书时间
	BackTime datetime not null Default getdate(), --还书时间
	money float not null, --超期扣款
	isback bit not null, --是否已还书 
	renew bit not null--是否续借
)

