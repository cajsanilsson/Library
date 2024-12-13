﻿using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BookQueries.GetAllBooks
{
    public class GetAllBooksQuery : IRequest<OperationResult<List<Book>>>
    {

    }
}
