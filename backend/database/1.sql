create schema if not exists exchange;


create table if not exists exchange.positions
(
    id int primary key generated always as identity,
    securitycode text not null,
    quantity decimal not null,
    primary key(id)
);

create unique index uniq_positions_securitycode on exchange.positions(securitycode);


create table if not exists exchange.transactions
(
    id int primary key generated always as identity,
    tradeid int not null,
    version int default null,
    securitycode text not null,
    quantity decimal not null,
    tradeoperationtype int not null,
    tradetype int not null
);
