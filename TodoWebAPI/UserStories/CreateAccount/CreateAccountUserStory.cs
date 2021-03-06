﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Domain.Repositories;
using Todo.Infrastructure;
using Todo.Infrastructure.Guids;
using TodoWebAPI.Models;

namespace TodoWebAPI.UserStories.CreateAccount
{
    public class CreateAccountUserStory : IRequestHandler<CreateAccountModel, Account>
    {
        private readonly IAccountRepository _accountRepository;

        public CreateAccountUserStory(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<Account> Handle(CreateAccountModel request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.FindAccountByEmailAsync(request.Email);

            if (account != null)
            {
                return account;
            }

            account = new Account(_accountRepository.NextId(), request.Email, PlanTiers.Free)
            {
                FullName = request.FullName,
                PictureUrl = request.PictureUrl,
            };

            _accountRepository.AddAccount(account);

            await _accountRepository.SaveChangesAsync();

            return account;
        }
    }
}
