﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SmartConvert.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvertController : ControllerBase
    {
        [HttpPost("video-to-audio")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ConvertVideoToAudio([FromForm] FileUploadDto fileUpload)
        {
            if (fileUpload.VideoFile == null)
                return BadRequest("No file provided.");

            try
            {
                var videoFile = fileUpload.VideoFile;

                // Save the uploaded video to a temporary location
                // Save the uploaded video to a temporary location
                var tempVideoPath = Path.Combine(Path.GetTempPath(), videoFile.FileName);
                using (var stream = new FileStream(tempVideoPath, FileMode.Create))
                {
                    await videoFile.CopyToAsync(stream);
                }

                // Define output audio file path
                var outputAudioPath = Path.ChangeExtension(tempVideoPath, ".mp3");

                // Set FFmpeg executables path for Linux
                Xabe.FFmpeg.FFmpeg.SetExecutablesPath("/usr/bin");

                // Convert video to audio
                var conversion = await Xabe.FFmpeg.FFmpeg.Conversions.FromSnippet.ExtractAudio(tempVideoPath, outputAudioPath);
                await conversion.Start();

                // Return the audio file to the user
                var audioBytes = await System.IO.File.ReadAllBytesAsync(outputAudioPath);
                var fileName = Path.GetFileName(outputAudioPath);

                // Clean up temporary files
                System.IO.File.Delete(tempVideoPath);
                System.IO.File.Delete(outputAudioPath);

                return File(audioBytes, "audio/mpeg", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
