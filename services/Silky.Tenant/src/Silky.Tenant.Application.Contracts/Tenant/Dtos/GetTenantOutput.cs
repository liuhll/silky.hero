﻿using System;

namespace Silky.Tenant.Application.Contracts.Tenant.Dtos;

public class GetTenantOutput : TenantDtoBase
{
    public long Id { get; set; }
    
    public DateTimeOffset CreatedTime { get; set; }
    
    public DateTimeOffset? UpdatedTime { get; set; }
}