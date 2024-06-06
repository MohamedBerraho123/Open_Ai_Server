﻿using Microsoft.AspNetCore.Mvc;
using OpenAI_UIR.Dtos;
using OpenAI_UIR.Models;

namespace OpenAI_UIR.Repository.Abstract
{
    public interface IQuestionRepository
    {
        Task<Question> CreateQuestionAsync(Question question);
    }
}
