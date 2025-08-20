using LiteMoney.Api.Requests;
using LiteMoney.Domain.Models;
using FastEndpoints;
using LiteMoney.Api.Responses.Category;

namespace LiteMoney.Api.Mappers;

public class AccountMapper : Mapper<UpdateRequest, Account, Account>
{
    public override Account ToEntity(UpdateRequest p) => new()
    {
        Id = p.Id,
        Name = p.Name,
        Balance = p.Balance,
        Icon = p.Icon,
        IconColor = p.IconColor,
        NameCurrency = p.NameCurrency
    };
}

public class AccountMapperV2 : Mapper<AccountRequest, AccountResponse, Account>
{
    public override Account ToEntity(AccountRequest r) => new()
    {
        Name = r.Name,
        Balance = r.Balance,
        Icon = r.Icon,
        IconColor = r.IconColor,
        NameCurrency = r.NameCurrency
    };

    public override AccountResponse FromEntity(Account e) => new(e.Id, e.Name, e.Balance, e.NameCurrency, e.Icon, e.IconColor);

    public List<AccountResponse> FromEntities(IEnumerable<Account> entities) =>
        entities.Select(a => new AccountResponse(a.Id, a.Name, a.Balance, a.NameCurrency, a.Icon, a.IconColor)).ToList();
}