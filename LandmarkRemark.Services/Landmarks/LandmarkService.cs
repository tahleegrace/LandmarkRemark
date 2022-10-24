using AutoMapper;

using LandmarkRemark.Entities;
using LandmarkRemark.Models.DTOs.Landmarks;
using LandmarkRemark.Models.Exceptions.Landmarks;
using LandmarkRemark.Repository.Landmarks;

namespace LandmarkRemark.Services.Landmarks
{
    /// <summary>
    /// Provides functionality for managing landmarks.
    /// </summary>
    public class LandmarkService : ILandmarkService
    {
        private readonly ILandmarkRepository _landmarkRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Creates a new instance of LandmarkService.
        /// </summary>
        /// <param name="landmarkRepository">The landmarks repository.</param>
        /// <param name="mapper">The mapper.</param>
        public LandmarkService(ILandmarkRepository landmarkRepository, IMapper mapper)
        {
            this._landmarkRepository = landmarkRepository;
            this._mapper = mapper;
        }

        /// <summary>
        /// Creates a new landmark.
        /// </summary>
        /// <param name="request">The landmark.</param>
        /// <param name="currentUserId">The ID of the current user.</param>
        /// <returns>The newly created landmark.</returns>
        public async Task<LandmarkDTO> Create(CreateLandmarkRequest request, int currentUserId)
        {
            if (request == null)
            {
                throw new LandmarkNotProvidedException();
            }

            var newLandmark = this._mapper.Map<Landmark>(request);
            newLandmark.UserId = currentUserId;
            newLandmark = await this._landmarkRepository.Create(newLandmark);

            var result = this._mapper.Map<LandmarkDTO>(newLandmark);
            return result;
        }

        /// <summary>
        /// Finds the landmarks for the current user.
        /// </summary>
        /// <param name="currentUserId">The ID of the current user.</param>
        /// <returns>The landmarks for the current user.</returns>
        public async Task<List<LandmarkDTO>> FindMyLandmarks(int currentUserId)
        {
            var landmarks = await this._landmarkRepository.FindByUserId(currentUserId);
            
            var result = this._mapper.Map<List<LandmarkDTO>>(landmarks);
            return result;
        }

        /// <summary>
        /// Returns all landmarks that have been added.
        /// </summary>
        /// <returns>All landmarks that have been added.</returns>
        public async Task<List<LandmarkDTO>> FindAll()
        {
            var landmarks = await this._landmarkRepository.FindAll();

            var result = this._mapper.Map<List<LandmarkDTO>>(landmarks);
            return result;
        }

        /// <summary>
        /// Finds the landmarks matching the specified search query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The landmarks matching the specified search query.</returns>
        public async Task<List<LandmarkDTO>> Search(string query)
        {
            var landmarks = await this._landmarkRepository.Search(query);

            var result = this._mapper.Map<List<LandmarkDTO>>(landmarks);
            return result;
        }
    }
}