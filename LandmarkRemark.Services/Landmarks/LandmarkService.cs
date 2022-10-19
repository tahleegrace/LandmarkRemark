using AutoMapper;

using LandmarkRemark.Entities;
using LandmarkRemark.Models.Landmarks;
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
        /// <returns>The newly created landmark.</returns>
        public async Task<LandmarkDTO> Create(CreateLandmarkRequest request)
        {
            // TODO: Check that the request is not null.

            var newLandmark = this._mapper.Map<Landmark>(request);
            await this._landmarkRepository.Create(newLandmark);

            var result = this._mapper.Map<LandmarkDTO>(newLandmark);
            return result;
        }

        /// <summary>
        /// Finds the landmarks for the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The landmarks for the specified user.</returns>
        public async Task<List<LandmarkDTO>> FindByUserId(int userId)
        {
            var landmarks = await this._landmarkRepository.FindByUserId(userId);
            
            var result = this._mapper.Map<List<LandmarkDTO>>(landmarks);
            return result;
        }
    }
}