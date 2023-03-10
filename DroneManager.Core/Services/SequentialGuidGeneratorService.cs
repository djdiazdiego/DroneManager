using System;
using Zaabee.SequentialGuid;

namespace DroneManager.Core.Services
{
    /// <summary>
    ///  Sequential Guid Generator
    /// </summary>
    public class SequentialGuidGeneratorService :
        ISqlGuidGenerator,
        IMySQLAndPostgreSQLGuidGenerator,
        IOracleGuidGenerator,
        ISQLiteGuidGenerator
    {
        /// <inheritdoc />
        Guid ISqlGuidGenerator.NewGuid() => SequentialGuidHelper.GenerateComb(SequentialGuidType.SequentialAtEnd);

        /// <inheritdoc />
        Guid IMySQLAndPostgreSQLGuidGenerator.NewGuid() => SequentialGuidHelper.GenerateComb(SequentialGuidType.SequentialAsString);

        Guid IOracleGuidGenerator.NewGuid() => SequentialGuidHelper.GenerateComb(SequentialGuidType.SequentialAsBinary);

        /// <inheritdoc />
        Guid ISQLiteGuidGenerator.NewBytesGuid() => SequentialGuidHelper.GenerateComb(SequentialGuidType.SequentialAsBinary);

        /// <inheritdoc />
        Guid ISQLiteGuidGenerator.NewStringGuid() => SequentialGuidHelper.GenerateComb(SequentialGuidType.SequentialAsString);

    }
}
