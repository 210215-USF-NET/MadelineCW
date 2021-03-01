create or replace procedure transfer(
   ait int
)
language plpgsql    
as $$
begin
while ait<5 loop
ait=ait+1;

insert into test(testinc) values (ait);
end loop;
end;$$


	call transfer(0)
	select * from test