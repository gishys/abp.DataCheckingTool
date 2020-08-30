using System;
using System.Collections.Generic;
using System.Text;

namespace DataCheckingTool.Application.Contracts
{
    public class CheckingResultDtos
    {
        public CheckingResultDto<TableCheckingResultDto> TableCheck
        {
            get;
            set;
        }
        public CheckingResultDto<FieldCheckingResultDto<Field>> FieldCheck
        {
            get;
            set;
        }
    }
}
