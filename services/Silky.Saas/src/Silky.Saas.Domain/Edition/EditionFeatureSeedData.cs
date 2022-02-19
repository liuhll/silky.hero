using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Entities.Configures;

namespace Silky.Saas.Domain;

public class EditionFeatureSeedData : IEntitySeedData<EditionFeature>
{
    public IEnumerable<EditionFeature> HasData(DbContext dbContext, Type dbContextLocator)
    {
        var initData = new List<EditionFeature>();
        initData.Add(new ()
        {
            Id = 1,
            EditionId = 1,
            FeatureId = 1,
            FeatureValue = "20"
        });
        initData.Add(new ()
        {
            Id = 2,
            EditionId = 1,
            FeatureId = 2,
            FeatureValue = "0"
        });
        initData.Add(new ()
        {
            Id = 3,
            EditionId = 1,
            FeatureId = 3,
            FeatureValue = "0"
        });
        initData.Add(new ()
        {
            Id = 4,
            EditionId = 2,
            FeatureId = 1,
            FeatureValue = "50"
        });
        initData.Add(new ()
        {
            Id = 5,
            EditionId = 2,
            FeatureId = 2,
            FeatureValue = "1"
        });
        initData.Add(new ()
        {
            Id = 6,
            EditionId = 2,
            FeatureId = 3,
            FeatureValue = "0"
        });
        initData.Add(new ()
        {
            Id = 7,
            EditionId = 3,
            FeatureId = 1,
            FeatureValue = "0"
        });
        initData.Add(new ()
        {
            Id = 8,
            EditionId = 2,
            FeatureId = 2,
            FeatureValue = "1"
        });
        initData.Add(new ()
        {
            Id = 9,
            EditionId = 2,
            FeatureId = 3,
            FeatureValue = "1"
        });
        return initData;
    }
}