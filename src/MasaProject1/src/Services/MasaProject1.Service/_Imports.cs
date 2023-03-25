﻿global using Dapr.Actors;
global using Dapr.Actors.Runtime;
global using Dapr.Actors.Client;
global using MasaProject1.Service.Actors;
global using FluentValidation.AspNetCore;
global using FluentValidation;
global using Masa.Contrib.Service.MinimalAPIs;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.OpenApi.Models;
global using Masa.BuildingBlocks.Data.UoW;
global using Masa.BuildingBlocks.Ddd.Domain.Entities;
global using Masa.BuildingBlocks.Ddd.Domain.Events;
global using Masa.BuildingBlocks.Ddd.Domain.Repositories;
global using Masa.BuildingBlocks.Ddd.Domain.Values;
global using Masa.BuildingBlocks.Dispatcher.Events;
global using Masa.Contrib.Data.UoW.EFCore;
global using Masa.Contrib.Ddd.Domain;
global using Masa.Contrib.Ddd.Domain.Repository.EFCore;
global using Masa.Contrib.Dispatcher.Events;
global using Masa.Contrib.Dispatcher.Events.Enums;
global using Masa.Contrib.Dispatcher.IntegrationEvents;
global using Masa.Contrib.Dispatcher.IntegrationEvents.Dapr;
global using Masa.Contrib.Dispatcher.IntegrationEvents.EventLogs.EFCore;
global using Masa.Contrib.ReadWriteSplitting.Cqrs.Commands;
global using Microsoft.EntityFrameworkCore;
global using MasaProject1.Service.Domain.Events;
global using MasaProject1.Service.Application.Orders.Commands;
global using MasaProject1.Service.Application.Orders.Queries;
global using MasaProject1.Service.Domain.Aggregates.Orders;
global using MasaProject1.Service.Domain.Repositories;
global using MasaProject1.Service.Domain.Services;
global using MasaProject1.Service.Infrastructure;
global using MasaProject1.Service.Infrastructure.Middleware;
