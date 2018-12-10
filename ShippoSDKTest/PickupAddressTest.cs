using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShippoSDK.Models;
using ShippoSDK.Services;

namespace ShippoSDKTest
{
    [TestClass]
    public class PickupAddressTest : BaseTest
    {
        private PickupAddressService _pickupAddressService;

        public PickupAddressTest()
        {
            _pickupAddressService = new PickupAddressService(Config);
        }

        [TestMethod]
        public void TestGet()
        {
            //create 3 address
            var pickupAddressModel1 = new PickupAddressModel
            {
                ContactName = "Chế Lan Viên",
                ContactPhone = "0987654321",
                LocationId = 37,
                DetailAddress = "Số 1 ngõ 2/12 đường Tiếng Hát Con Tầu"
            };

            var pickupAddressModel2 = new PickupAddressModel
            {
                ContactName = "Nguyên Ngọc",
                ContactPhone = "0987654321",
                LocationId = 37,
                DetailAddress = "Số 1 ngõ 2/12 Phố Đất Nước Đứng Lên"
            };

            var pickupAddressModel3 = new PickupAddressModel
            {
                ContactName = "Nguyễn Đình Thi",
                ContactPhone = "0987654321",
                LocationId = 37,
                DetailAddress = "Số 1 ngõ 2/12 Phố Ông Đồ"
            };

            CreatePa(pickupAddressModel1);
            CreatePa(pickupAddressModel2);
            CreatePa(pickupAddressModel3);

            var expected = _pickupAddressService.Get().Result;
            
            Assert.IsTrue(expected.Count > 0, "No data location return.");
        }

        [TestMethod]
        public void TestCreate()
        {
            var pickupAddressModel = new PickupAddressModel
            {
                ContactName = "Lưu Quang Vũ",
                ContactPhone = "0987654321",
                LocationId = 37,
                DetailAddress = "Số 1 ngõ 2/12 Phạm Hồng Thái"
            };

            var actual = CreatePa(pickupAddressModel);

            Assert.AreEqual("9.37", actual.LocationIdsPath);
            Assert.AreEqual(pickupAddressModel.ContactPhone, actual.ContactPhone);
        }

        [TestMethod]
        public void TestDetail()
        {
            var pickupAddressModel = new PickupAddressModel
            {
                ContactName = "Xuân Quỳnh",
                ContactPhone = "0987654321",
                LocationId = 37,
                DetailAddress = "Số 1 ngõ 2/12 Phố Ta"
            };

            var expected = CreatePa(pickupAddressModel);

            var actual = _pickupAddressService.Detail(expected.Id).Result;

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.ContactPhone, actual.ContactPhone);
        }

        [TestMethod]
        public void TestEdit()
        {
            var pickupAddressModel = new PickupAddressModel
            {
                ContactName = "Xuân Quỳnh",
                ContactPhone = "0987654321",
                LocationId = 37,
                DetailAddress = "Số 1 ngõ 2/12 Phố Ta"
            };

            var samplePa = CreatePa(pickupAddressModel);

            var updatePa = new PickupAddressModel
            {
                ContactPhone = "0381234567",
                LocationId = 37,
                DetailAddress = "Số 1 ngõ 3/12 Thuyền và Biển"
            };
            
            var edit = _pickupAddressService.Edit(samplePa.Id, updatePa).Result;
            var actual = _pickupAddressService.Detail(samplePa.Id).Result;
            
            Assert.AreEqual(updatePa.ContactPhone, actual.ContactPhone);
            Assert.AreEqual(updatePa.DetailAddress, actual.DetailAddress);
        }


        private PickupAddressModel CreatePa(PickupAddressModel param)
        {
            var pickupAddressModel = new PickupAddressModel
            {
                ContactName = "Xuân Quỳnh",
                ContactPhone = "0987654321",
                LocationId = 37,
                DetailAddress = "Số 1 ngõ 2/12 Phố Ta"
            };

            if (param != null) pickupAddressModel = param;
            var data = _pickupAddressService.Create(pickupAddressModel).Result;

            return data;
        }
    }
}