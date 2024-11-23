﻿using Application.BookCommands.AddBookCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Domain;

namespace Application.BookQueries.GetBookById
{
    public class GetBookByIdQuery : IRequest<Book>
    {
        public GetBookByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
