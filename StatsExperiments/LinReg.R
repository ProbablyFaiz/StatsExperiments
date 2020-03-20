# Created by: faiz
# Created on: 3/19/20

regData <- read.csv('TestData.csv')
lReg <- lm(Input~Output, data=regData)
info <- summary(lReg)
cat(paste("r-squared", "adjusted-r-squared", "f-statistic", sep=","))
cat("\n")
cat(paste(info$r.squared, info$adj.r.squared, info$fstatistic["value"], sep=","))
