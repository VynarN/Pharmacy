﻿using Pharmacy.Application.Common.AppObjects;
using Pharmacy.Application.Common.DTO.Out;
using Pharmacy.Application.Common.Queries;
using System.Collections.Generic;

namespace Pharmacy.Application.Common.Interfaces.HelpersInterfaces
{
    public interface IPaginationHelper
    {
        PaginatedResponse<MedicamentOutDto> FormMedicamentsPaginatedResponse(int medicamentsCount, 
                                                   IEnumerable<MedicamentOutDto> medicamentsDto,
                                                   PaginationQuery paginationQuery, 
                                                   MedicamentFilterQuery medicamentFilterQuery);
    }
}
