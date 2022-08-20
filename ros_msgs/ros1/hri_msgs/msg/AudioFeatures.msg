# This message encodes the 16 audio features selected
# in the INTERSPEECH2009 challenge.
# 
# Reference: Schuller, Steidl, Batliner, The INTERSPEECH 2009 Emotion Challenge
# 
# They can be extract using the OpenSMILE toolkit with the present
# IS09_emotion.conf

float32 ZCR # zero-crossing rate from the time signal
float32 RMS # root mean square frame energy
float32 pitch # pitch frequency (normalised to 500 Hz)
float32 HNR # harmonics-to-noise ratio by autocorrelation function
float32[] MFCC # mel-frequency cepstral coefficients 1 to 12
