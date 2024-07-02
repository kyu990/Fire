using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FFmpeg.AutoGen;
using Org.BouncyCastle.Security;
using Mysqlx.Notice;

// OpenCV - VideoWriter 클래스 사용해야 할 것 같음. 이거 너무 어려움.

namespace MobleFinal._Service
{
    internal class Formatter
    {
        // FFmpeg 바이너리 등록, 나는 Nuget에서 FFmpeg.AutoGen 설치를 받아서 이 과정이 필요 없음.
        // FFmpegBinariesHelper.RegisterFFmpegBinaries();

        public Formatter()
        {

        }

        public void CodecEncoder() // < 메인문에서 갖다 쓸 놈
        {
            // 파일명 설정 미 인코딩 함수 호출
            string dirPath = Path.GetDirectoryName(typeof(Program).Assembly.Location) ?? "";
            string filename = Path.Combine(dirPath, "a.mp4");

            VideoEncode(filename, AVCodecID.AV_CODEC_ID_H264);
        }

        static unsafe void encode(AVCodecContext* enc_ctx, AVFrame* frame, AVPacket* pkt, BinaryWriter output)
        {
            int ret;

            ret = ffmpeg.avcodec_send_frame(enc_ctx, frame);
            if (ret == 0)
            {
                return;
            }

            while(ret >= 0)
            {
                // avcodec_receive_paket()을 통해 인코더로부터 압축된 패킷을 수신함.
                ret = ffmpeg.avcodec_receive_packet(enc_ctx, pkt);
                // 인코딩이 완료되었거나, 추가적인 패킷을 받을 수 없으면 메서드 종료.
                if (ret == ffmpeg.AVERROR(ffmpeg.EAGAIN) || ret == ffmpeg.AVERROR_EOF)
                {
                    return;
                }
                // 오류를 발생한 것이라면, 메서드 종료. >> 인코딩 재시도 or VideoEncode 메서드 손 봐야함.
                else if (ret < 0)
                {
                    return;
                }

                // pkt->data은 수신된 패킷의 데이터가 저장된 포인터.
                // pkt->size는 해당 데이터의 크기.
                // 이들을 BinaryWriter를 사용해 출력 파일에 기록하는 과정 수행.
                output.Write(new ReadOnlySpan<byte>(pkt->data, pkt->size));

                // 메모리 누수 방지를 위한 패킷 해제
                ffmpeg.av_packet_unref(pkt);
            }
        }

        // unsafe 키워드를 사용함으로써 FFmpeg.AutoGen 라이브러리를 사용해 FFmpeg의 네이티브 코드와 상호작용함.
        static unsafe void VideoEncode(string filename, AVCodecID codecID)
        {
            // 포인터와 고정 크기 버퍼는 안전하지 않은 컨텍스트에서만 사용 가능해서 unsafe 키워드 필요.
            AVCodec* pCodec = null;
            AVCodecContext* pContext = null;
            AVFrame* pframe = null;
            AVPacket* ppacket = null;
            byte[] endCode = new byte[] { 0, 0, 1, 0xb7 };

            using BinaryWriter output = new BinaryWriter(new FileStream(filename, FileMode.Create));


            do
            {
                pCodec = ffmpeg.avcodec_find_encoder(codecID);
                if(pCodec == null)
                {
                    break;
                }

                pContext = ffmpeg.avcodec_alloc_context3(pCodec);
                if(pContext == null)
                {
                    break;
                }

                ppacket = ffmpeg.av_packet_alloc();
                if(ppacket == null)
                {
                    break;
                }
                // 비트레이트 720p = 1500~4000, 1080p = 3000~6000
                // 스트리밍의 경우 웹쪽 네트워크 대역폭을 고려해야함.
                pContext->bit_rate = 4000;
                pContext->width = 640; // 웹이랑 윈폼이랑 맞춰야할듯
                pContext->height = 480; // 웹이랑 윈폼이랑 맞춰야할듯

                // time_base는 비디오의 시간 기반 설정.
                // 시간 기반은 프레임의 시간 단위를 정의하며 프레임의 표시 시간을 계산하는데 사용
                // num이 분자고 den이 분모임. 따라서 1/25인 것.
                pContext->time_base = new AVRational { num = 1, den = 25 };

                // framerate는 비디오의 프레임 속도 설정.
                // 프레임 속도는 1초동안 표시되는 프레임의 수
                // 25/1 이기 때문에 초당 25프레임.
                pContext->framerate = new AVRational { num = 25, den = 1 };

                // gop_size는 Group of Pictures(GOP)의 크기를 설정함.
                // GOP는 비디오 스트림 내의 프레임들의 그룹을 의미한다.
                // ex) 값이 10으로 설정이 된다면, 인코더는 매 10프레임 마다 하나의 I-프레임 생성.
                // I-프레임은 전체 이미지 포함을 의미한다.
                pContext->gop_size = 10;

                // max_b_frames는 한 쌍의 참조 프레임 사이에 배치될 최대 B-프레임 수를 설정.
                // B-프레임은 양쪽 방향으로 참조 프레임을 사용하여 압축률을 높히는 역할을 한다.
                // 값 1로 설정될 경우 인코더는 한 쌍의 참조 프레임 사이에 최대 1개의 B-프레임을 삽입한다.
                pContext->max_b_frames = 1;


                // pix_fmt는 비디오 프레임의 픽셀 형식을 지정.
                // YUV420P는 YUV 색 공간에서 4:2:0 샘플링을 사용한다.
                // 이는 인간의 시각 시스템이 색상보다 밝기에 더 민감하다는 점을 이용하여 색상 데이터를 효율적으로 압축하는 방식이다.
                pContext->pix_fmt = AVPixelFormat.AV_PIX_FMT_YUV420P;

                if(codecID == AVCodecID.AV_CODEC_ID_H264)
                {
                    ffmpeg.av_opt_set(pContext->priv_data, "preset", "slow", 0);
                }
                
                if(ffmpeg.avcodec_open2(pContext, pCodec, null) < 0)
                {
                    break;
                }

                pframe = ffmpeg.av_frame_alloc();

                if (pframe == null)
                {
                    break;
                }

                pframe->format = (int)pContext->pix_fmt;
                pframe->width = pContext->width;
                pframe->height = pContext->height;

                int ret = ffmpeg.av_frame_get_buffer(pframe, 0);
                if (ret < 0)
                {
                    break;
                }

                int x = 0;
                int y = 0;

                for (int i = 0; i < 25 * 10; i++)
                {
                    ret = ffmpeg.av_frame_make_writable(pframe);
                    if (ret < 0)
                    {
                        break;
                    }

                    for (y = 0; y < pContext->height; y++)
                    {
                        for (x = 0; x < pContext->width; x++)
                        {
                            pframe->data[0][y * pframe->linesize[0] + x] = (byte)(x + y + i * 3);
                        }
                    }

                    for (y = 0; y < pContext->height / 2; y++)
                    {
                        for (x = 0; x < pContext->width / 2; x++)
                        {
                            pframe->data[1][y * pframe->linesize[1] + x] = (byte)(128 + y + i * 2);
                            pframe->data[2][y * pframe->linesize[2] + x] = (byte)(64 + y + i * 5);
                        }
                    }

                    pframe->pts = i;
                    encode(pContext, pframe, ppacket, output);
                }
                encode(pContext, null, ppacket, output);
            } while (false);

            
            if(pCodec->id == AVCodecID.AV_CODEC_ID_MPEG1VIDEO || pCodec->id == AVCodecID.AV_CODEC_ID_MPEG2VIDEO)
            {
                output.Write(endCode, 0, endCode.Length);
            }

            if (pframe !=  null)
            {
                ffmpeg.av_frame_free(&pframe);
            }

            if (ppacket != null)
            {
                ffmpeg.av_packet_free(&ppacket);
            }

            if(ppacket != null)
            {
                ffmpeg.avcodec_free_context(&pContext);
            }

        }
    }
}
