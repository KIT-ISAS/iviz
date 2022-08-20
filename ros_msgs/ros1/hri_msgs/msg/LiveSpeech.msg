# This message encodes the live result of a speech recognition process.
# A series of incremental results might be provided, until a final recognition
# hypothesis is returned.

Header header

string incremental # incremental recognition results
string final # final recognised text

float64 confidence # confidence of the final recognised speech, [0-1]


