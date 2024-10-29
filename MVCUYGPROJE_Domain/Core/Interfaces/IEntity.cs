﻿using MVCUYGPROJE_Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCUYGPROJE_Domain.Core.Interfaces;

public interface IEntity
{
    public Guid Id { get; set; }
    public Status Status { get; set; }
}
