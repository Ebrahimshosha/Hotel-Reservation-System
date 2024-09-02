//global

global using System.Diagnostics;
global using System.Data;
global using static System.Net.Mime.MediaTypeNames;
global using System.Collections.Generic;

global using Microsoft.AspNetCore.Cors.Infrastructure;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;

global using Microsoft.EntityFrameworkCore.Metadata.Internal;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.EntityFrameworkCore;

global using AutoMapper;
global using AutoMapper.QueryableExtensions;

global using Hotel_Reservation_System.Middlewares;

global using Hotel_Reservation_System.Models;
global using Hotel_Reservation_System.Data;
global using Hotel_Reservation_System.Repositories;
global using Hotel_Reservation_System.Helpers;

global using Hotel_Reservation_System.Services.RoomService;
global using Hotel_Reservation_System.Services.FacilitiesServices;

global using Hotel_Reservation_System.DTO.Room;
global using Hotel_Reservation_System.DTO.Facility;
global using Hotel_Reservation_System.DTO.RoomFacility;

global using Hotel_Reservation_System.Mediators.RoomMediator;
global using Hotel_Reservation_System.Mediators.FacilityMediator;

global using Hotel_Reservation_System.ViewModels.Room;
global using Hotel_Reservation_System.ViewModels.FacilitiesViewModel;

global using Hotel_Reservation_System.Services.RoomFacilityService;
global using Hotel_Reservation_System.Services.RoomImages;


