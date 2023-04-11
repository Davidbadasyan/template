// Global using directives

global using System.Reflection;
global using Autofac;
global using Autofac.Extensions.DependencyInjection;
global using Common.Application;
global using Common.Application.IntegrationEvents;
global using Common.Application.Middlewares;
global using Common.Application.Services;
global using Common.Domain.EventBus.Abstractions;
global using Common.Infrastructure.Behaviours;
global using Common.Infrastructure.DbContext;
global using Common.Infrastructure.EventBus.IntegrationEventLogger.Services;
global using Common.Infrastructure.EventBus.RabbitMQ;
global using Common.Infrastructure.Extensions;
global using MediatR;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Orders.API.AutofacModules;
global using Orders.API.Extensions;
global using Orders.Application.Commands;
global using Orders.Application.IntegrationEventHandlers;
global using Orders.Application.Mappings;
global using Orders.Application.Queries;
global using Orders.Domain.AggregatesModel.OrderAggregate;
global using Orders.Infrastructure.DbContexts;
global using Orders.Infrastructure.Repositories;
global using Orders.Infrastructure.Repositories.Queries;