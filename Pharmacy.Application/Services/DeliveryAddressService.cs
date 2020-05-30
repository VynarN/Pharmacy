﻿using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Domain.Entites;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Application.Services
{
    public class DeliveryAddressService : IDeliveryAddressService
    {
        private readonly IRepository<DeliveryAddress> _deliveryAddressRepository;
        private readonly IRepository<Order> _orderRepository;

        public DeliveryAddressService(IRepository<DeliveryAddress> deliveryAddressRepository, IRepository<Order> orderRepository)
        {
            _deliveryAddressRepository = deliveryAddressRepository;
            _orderRepository = orderRepository;
        }

        public async Task<int> CreateDeliveryAddress(DeliveryAddress deliveryAddress)
        {
            await _deliveryAddressRepository.Create(deliveryAddress);

            return _deliveryAddressRepository.GetByPredicate(da => da.Country.Equals(deliveryAddress.Country) 
                                                            && da.Region.Equals(deliveryAddress.Region) 
                                                            && da.City.Equals(deliveryAddress.City)
                                                            && da.Street.Equals(deliveryAddress.Street)
                                                            && da.ZipCode == deliveryAddress.ZipCode).First().Id;
        }

        public IEnumerable<DeliveryAddress> GetDeliveryAddresses(string userId)
        {
            return _orderRepository.GetWithInclude(order => order.DeliveryAddress)
                .Where(order => order.UserId.Equals(userId))
                .Select(order => order.DeliveryAddress).Distinct();
        }
    }
}
