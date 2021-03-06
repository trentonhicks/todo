﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.Repositories;
using Todo.Infrastructure.Email;

namespace TodoWebAPI.CronJob
{
    public class DueDateJob : CronJobService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DueDateJob> _logger;
        private readonly IEmailService _emailService;
        private readonly DapperQuery _dapperQuery;

        public DueDateJob(
            IConfiguration configuration,
            IScheduleConfig<DueDateJob> config,
            ILogger<DueDateJob> logger,
            IEmailService emailService,
            DapperQuery dapperQuery)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _configuration = configuration;
            _logger = logger;
            _emailService = emailService;
            _dapperQuery = dapperQuery;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Due Date Starts.");
            return base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            var items = await _dapperQuery.GetItemsFromListItemsAsync();

            foreach (var item in items)
            {
                if (item.DueDate?.Date == DateTime.Now.Date && item.DueDate?.Year == DateTime.Now.Year)
                {
                    var contributors = await _dapperQuery.GetContributorsByListIdAsync(item.ListId.GetValueOrDefault());

                    foreach(var contributor in contributors)
                    {
                        var email = new Email()
                        {
                            To = contributor,
                            From = _configuration.GetSection("Emails")["Notifications"],
                            Subject = $"{item.Name} is due today. | {item.DueDate}",
                            Body = $"{item.Name} is due today. Better hurry!"
                        };
                        await _emailService.SendEmailAsync(email);
                    }
                   
                }
            }

            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} Due Date Job is working.");
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Due Date Job is Stopping");
            return base.StopAsync(cancellationToken);
        }
    }
}
